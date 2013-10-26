using System;
using System.Threading.Tasks;
using FluorineFx.Messaging.Messages;
using FluorineFx.Net;
using LolSession = com.riotgames.platform.login.Session;

namespace Lollipop.Session
{
    public class SubscriptionManager : IDisposable
    {
        private readonly SessionManager _sessionManager;
        private bool _disposed;

        public SubscriptionManager(SessionManager sessionManager)
        {
            if (sessionManager == null) throw new ArgumentNullException("sessionManager");
            _sessionManager = sessionManager;
        }

        public async Task Subscribe(NetConnection connection)
        {
            var session = _sessionManager.CurrentSession;
            if (session == null)
                throw new InvalidOperationException("The session has not been created, yet.  Unable to subscribe.");

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

        public static CommandMessage CreateSubscribeMessage(string subTopic, string clientId)
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

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (!disposing) return;

            _disposed = true;
        }
    }
}