using System;
using System.Threading.Tasks;
using com.riotgames.platform.login;
using FluorineFx;
using FluorineFx.Messaging.Messages;
using FluorineFx.Net;
using log4net;

namespace Lollipop.Session
{
    public class LeagueConnection : ILeagueConnection
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof (LeagueConnection));

        private bool _disposed;
        private object _routingObject;
        private Action<IRtmpConnection> _setup;
        private SessionManager _sessions;
        private HeartbeatManager _heartbeat;
        private SubscriptionManager _subscriptions;

        public bool IsConnected { get; private set; }

        public IRtmpConnection Connection { get; private set; }

        public LeagueConnection()
        {
            _sessions = new SessionManager();
            _heartbeat = new HeartbeatManager(_sessions);
            _subscriptions = new SubscriptionManager(_sessions);
        }

        public LeagueConnection RouteEventsTo(object obj)
        {
            _routingObject = obj;
            return this;
        }

        public LeagueConnection Setup(Action<IRtmpConnection> action)
        {
            _setup = action;
            return this;
        }

        public async Task<bool> Connect(LeagueRegion region, AuthenticationCredentials credentials)
        {
            if (Connection == null)
            {
                var connection = Create();
                var connected = await new ConnectionTask(connection).Initiate(region.ServerUri);
                if (!connected)
                    return false;

                Connection = connection;
            }

            Connection.AddHeader(MessageBase.RequestTimeoutHeader, false, 60);
            Connection.AddHeader(MessageBase.FlexClientIdHeader, false, Guid.NewGuid().ToString());
            Connection.AddHeader(MessageBase.EndpointHeader, false, Invocation.EndpointName);

            // Setup the session
            await _sessions.Login(Connection, credentials);

            // Subscribe to all of the fun messages
            await _subscriptions.Subscribe(Connection);

            // Startup the heartbeat
            _heartbeat.Start(Connection);

            return (IsConnected = true);
        }

        public bool Disconnect()
        {
            try
            {
                _heartbeat.Cancel();

                if (Connection != null)
                {
                    _sessions.Logout(Connection).Wait(TimeSpan.FromSeconds(30));

                    Connection.Close();
                    Connection = null;
                }

                IsConnected = false;
            }
            catch (Exception)
            {
                IsConnected = false;
                throw;
            }
            finally
            {
                IsConnected = false;
            }

            return IsConnected;
        }

        private IRtmpConnection Create()
        {
            var connection = new NetConnectionWrapper(new NetConnection
            {
                ObjectEncoding = ObjectEncoding.AMF3
            });

            if (_setup != null)
            {
                _setup(connection);
            }
            if (_routingObject != null)
            {
                connection.Client = _routingObject;
            }

            return connection;
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

            try
            {
                if (Connection != null)
                    Disconnect();

                if (_heartbeat != null)
                {
                    _heartbeat.Dispose();
                }
                if (_subscriptions != null)
                {
                    _subscriptions.Dispose();
                }
                if (_sessions != null)
                {
                    _sessions.Dispose();
                }
            }
            finally
            {
                _disposed = true;
                _heartbeat = null;
                _subscriptions = null;
                _sessions = null;
                _setup = null;
                _routingObject = null;

                Connection = null;
            }
        }
    }
}