using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using com.riotgames.platform.login;
using FluorineFx.Messaging.Messages;
using FluorineFx.Net;
using log4net;
using Lollipop.Session;

namespace Lollipop.Auth
{
    public class LeagueConnectionHandler : IConnectionHandler
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof (LeagueConnectionHandler));

        public com.riotgames.platform.login.Session Session { get; private set; }

        public CancellationTokenSource Heartbeat { get; private set; }

        public bool IsConnected { get; private set; }

        public async Task<bool> Connect(NetConnection connection, AuthenticationCredentials credentials)
        {
            connection.AddHeader(MessageBase.RequestTimeoutHeader, false, 60);
            connection.AddHeader(MessageBase.FlexClientIdHeader, false, Guid.NewGuid().ToString());
            connection.AddHeader(MessageBase.EndpointHeader, false, Invocation.EndpointName);

            var session =
                await new Invocation<com.riotgames.platform.login.Session>("loginService", "login", credentials)
                          .Execute(connection);

            // todo: Figure out the type in Session.accountSummary
            return await AuthorizeSession(connection, session);
        }

        public async Task<bool> Disconnect(NetConnection connection)
        {
            await TryLogout(connection);

            TryKill(Heartbeat);
            
            connection.Close();

            return (IsConnected = false);
        }

        private async Task<bool> AuthorizeSession(NetConnection connection, com.riotgames.platform.login.Session session)
        {
            connection.AddHeader(MessageBase.FlexClientIdHeader, false, session.token);

            // note: username is case sensitive here for some reason...
            var token = string.Format("{0}:{1}", session.accountSummary.username, session.token);
            var tokenEncoded = Encoding.ASCII.GetBytes(token);
            var body = Convert.ToBase64String(tokenEncoded);
            var msg = new CommandMessage
            {
                operation = CommandMessage.LoginOperation,
                clientId = connection.ClientId,
                correlationId = null,
                messageId = Guid.NewGuid().ToString(),
                body = body,
                destination = string.Empty
            };

            await (new Invocation<string>("auth", msg).Execute(connection));

            await Subscribe(connection, session);

            Heartbeat = StartHeartBeat(connection, session);

            return (IsConnected = true);
        }

        private async Task TryLogout(NetConnection connection)
        {
            try
            {
                var invoke = new Invocation<AcknowledgeMessage>("loginService", "logout", Session.token);
                await invoke.Execute(connection);
            }
            catch (Exception ex)
            {
                Log.Warn("There was an error attempting to logout.", ex);
            }
        }

        private static void TryKill(CancellationTokenSource token)
        {
            try
            {
                if (token == null) return;
                if (token.IsCancellationRequested) return;

                token.Cancel();
            }
            catch (Exception ex)
            {
                Log.Warn("There was an error attempting to cancel the process.", ex);
            }
        }

        private static async Task Subscribe(NetConnection connection, com.riotgames.platform.login.Session session)
        {
            var bcMessage = CreateSubscribeMessage("bc", "bc-" + session.accountSummary.accountId);
            var cnMessage = CreateSubscribeMessage("cn-" + session.accountSummary.accountId,
                                                   "cn-" + session.accountSummary.accountId);
            var gnMessage = CreateSubscribeMessage("gn-" + session.accountSummary.accountId,
                                                   "gn-" + session.accountSummary.accountId);

            var bcInvoke = new Invocation<object>(bcMessage);
            await bcInvoke.Execute(connection);

            var cnInvoke = new Invocation<object>(cnMessage);
            await cnInvoke.Execute(connection);

            var gnInvoke = new Invocation<object>(gnMessage);
            await gnInvoke.Execute(connection);
        }

        private static CommandMessage CreateSubscribeMessage(string subTopic, string clientId)
        {
            var message = new CommandMessage
            {
                operation = CommandMessage.SubscribeOperation,
                clientId = clientId,
                correlationId = string.Empty,
                messageId = Guid.NewGuid().ToString(),
                destination = "messagingDestination"
            };
            message.headers[MessageBase.EndpointHeader] = Invocation.EndpointName;
            message.headers[AsyncMessage.SubtopicHeader] = subTopic;

            return message;
        }

        private static CancellationTokenSource StartHeartBeat(NetConnection connection,
                                                              com.riotgames.platform.login.Session session)
        {
            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;
            Task.Run(async () =>
            {
                var count = 1;
                while (!token.IsCancellationRequested)
                {
                    await Task.Delay(TimeSpan.FromMilliseconds(120000), token);
                    if (token.IsCancellationRequested)
                        return;

                    var date = DateTime.UtcNow.ToString("ddd MMM d yyyy HH:mm:ss 'GMTZ'");
                    try
                    {
                        var invoke = new Invocation<int>("loginService", "performLCDSHeartBeat",
                                                         session.accountSummary.accountId, session.token, count, date);
                        await invoke.Execute(connection);
                    }
                    catch (AggregateException agg)
                    {
                        agg.Handle(e => e is TaskCanceledException);
                    }

                    count++;
                }
            }, token);
            return tokenSource;
        }
    }
}