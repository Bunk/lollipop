using System;
using System.Threading;
using System.Threading.Tasks;
using FluorineFx.Net;
using LolSession = com.riotgames.platform.login.Session;

namespace Lollipop.Session
{
    public class HeartbeatManager : IDisposable
    {
        private readonly SessionManager _sessionManager;
        private CancellationTokenSource _tokenSource;
        private Task _heartbeat;
        private bool _disposed;

        public HeartbeatManager(SessionManager sessionManager)
        {
            if (sessionManager == null) throw new ArgumentNullException("sessionManager");
            _sessionManager = sessionManager;
        }

        public void Start(NetConnection connection)
        {
            if (_heartbeat != null) 
                return;
            if (_tokenSource != null)
                return;

            _tokenSource = new CancellationTokenSource();
            _heartbeat = Task.Run(() => Run(connection), _tokenSource.Token);
        }

        public void Cancel()
        {
            if (_tokenSource != null)
                _tokenSource.Cancel();

            _heartbeat = null;
        }

        private async Task Run(NetConnection connection)
        {
            var count = 1;
            var token = _tokenSource.Token;
            while (!token.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromMilliseconds(120000), token);
                if (token.IsCancellationRequested)
                    return;

                var date = DateTime.UtcNow.ToString("ddd MMM d yyyy HH:mm:ss 'GMTZ'");
                try
                {
                    var session = _sessionManager.CurrentSession;
                    if (session == null)
                        continue;

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
            _tokenSource = null;
            _heartbeat = null;
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
            if (_tokenSource != null)
                _tokenSource.Cancel();

            _heartbeat = null;
        }
    }
}