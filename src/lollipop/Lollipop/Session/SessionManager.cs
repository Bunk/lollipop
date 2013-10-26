using System;
using System.Text;
using System.Threading.Tasks;
using com.riotgames.platform.login;
using FluorineFx.Messaging.Messages;
using FluorineFx.Net;
using LolSession = com.riotgames.platform.login.Session;

namespace Lollipop.Session
{
    public class SessionManager : IDisposable
    {
        private bool _disposed;
        private Action _action;

        public AuthenticationCredentials Credentials { get; private set; }

        public LolSession CurrentSession { get; private set; }

        public async Task<LolSession> Login(NetConnection connection, AuthenticationCredentials credentials)
        {
            Credentials = credentials;

            var invoke = new Invocation<LolSession>("loginService", "login", credentials);
            CurrentSession = await invoke.Execute(connection);

            await AuthorizeSession(connection, CurrentSession);

            if (_action != null)
                _action();

            return CurrentSession;
        }

        public async Task Logout(NetConnection connection)
        {
            if (CurrentSession == null)
                return;
            if (string.IsNullOrWhiteSpace(CurrentSession.token))
                return;

            var invoke = new Invocation<AcknowledgeMessage>("loginService", "logout", CurrentSession.token);
            var message = await invoke.Execute(connection);

            return;
        }

        private static async Task AuthorizeSession(NetConnection connection, LolSession session)
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

            await new Invocation<string>("auth", msg).Execute(connection);
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

            CurrentSession = null;

            _action = null;
            _disposed = true;
        }
    }
}