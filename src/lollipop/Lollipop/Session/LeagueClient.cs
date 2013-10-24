using System;
using System.Globalization;
using System.Reflection;
using System.Threading.Tasks;
using com.riotgames.platform.login;
using FluorineFx;
using FluorineFx.Messaging.Messages;
using FluorineFx.Net;
using log4net;
using Lollipop.Auth;
using Lollipop.Utility;

namespace Lollipop.Session
{
    public class LeagueClient : IFlashRemotingClient
    {
        private const string ClientVersion = "3.12.13_09_24_17_41";
        private const string ClientDomain = "lolclient.lol.riotgames.com";

        private static readonly ILog Log = LogManager.GetLogger(typeof (LeagueClient));

        private readonly IAuthorize _authorize;
        private readonly IConnectionHandler _connectionHandler;
        private readonly ILocateServerIP _locateServer;
        private NetConnection _connection;
        private AuthenticationCredentials _credentials;
        private LeagueRegion _region;

        static LeagueClient()
        {
            TypeHelper.AddTargetAssembly(Assembly.GetAssembly(typeof (AuthenticationCredentials)));
        }

        public LeagueClient(ILocateServerIP locateServer, IAuthorize authorize, IConnectionHandler handler)
        {
            if (locateServer == null) throw new ArgumentNullException("locateServer");
            if (authorize == null) throw new ArgumentNullException("authorize");

            _connectionHandler = handler;
            _locateServer = locateServer;
            _authorize = authorize;
        }

        public string AuthToken { get; private set; }

        public string ServerIp { get; private set; }

        public bool IsConnected { get; private set; }

        public IFlashRemotingClient Use(LeagueRegion region, string username, string password)
        {
            if (region == null) throw new ArgumentNullException("region");

            if (IsConnected)
                Disconnect();

            _region = region;
            _credentials = new AuthenticationCredentials
            {
                clientVersion = ClientVersion,
                domain = ClientDomain,
                locale = CultureInfo.CurrentCulture.ToString().Replace("-", "_"),
                ipAddress = ServerIp ?? "127.0.0.1",
                username = username.ToLowerInvariant(),
                password = password
            };

            return this;
        }

        public async Task Login()
        {
            if (IsConnected)
                return;

            ServerIp = await _locateServer.Locate(_region);
            AuthToken = await _authorize.GetAuthToken(_region, _credentials.username, _credentials.password);

            if (!string.IsNullOrWhiteSpace(AuthToken))
                _credentials.authToken = AuthToken;

            if (!string.IsNullOrWhiteSpace(ServerIp))
                _credentials.ipAddress = ServerIp;
        }

        public Task Connect()
        {
            var t = new TaskCompletionSource<bool>();

            if (_connection != null && IsConnected)
            {
                t.SetResult(true);
            }
            else
            {
                _connection = new NetConnection
                {
                    ObjectEncoding = ObjectEncoding.AMF3,
                    Client = this // invoke callbacks on this object
                };

                _connection.OnConnect += (sender, args) => HandleConnection(t);
                _connection.OnDisconnect += (sender, args) => HandleDisconnect(t);
                _connection.NetStatus += NetStatus;

                try
                {
                    _connection.Connect(_region.ServerUri.ToString());
                }
                catch (Exception ex)
                {
                    t.SetException(ex);
                }
            }

            return t.Task;
        }

        public async Task Reconnect()
        {
            while (!IsConnected)
            {
                await Connect();
            }
        }

        public Task Disconnect()
        {
            return Task.Factory.StartNew(() =>
            {
                if (_connection != null)
                {
                    // todo: logout
                    _connection.Close();
                    _connection = null;
                }

                IsConnected = false;
            });
        }

        public Task<T> Call<T>(string service, string method, params object[] parameters)
        {
            var invocation = new Invocation<T>(service, method, parameters);
            return invocation.Execute(_connection);
        }

        public void receive(AsyncMessage message)
        {
            Log.InfoFormat("RECV: {0}", message);
        }

        /// <summary>
        ///     Called when an event is sent back to us from the server (async)
        /// </summary>
        private static void NetStatus(object sender, NetStatusEventArgs netStatusEventArgs)
        {
            Log.InfoFormat("RECV: {0}", netStatusEventArgs.Info.AsString());

            if (netStatusEventArgs.Exception != null)
                Log.Error("RECV ERROR: ", netStatusEventArgs.Exception);

            // todo: Handle broadasts, server disconnects, etc
        }

        private async Task HandleConnection(TaskCompletionSource<bool> continuation)
        {
            try
            {
                IsConnected = await _connectionHandler.Connect(_connection, _credentials);
                continuation.TrySetResult(true);
            }
            catch (Exception ex)
            {
                continuation.TrySetException(ex);
            }
        }

        private async Task HandleDisconnect(TaskCompletionSource<bool> continuation)
        {
            try
            {
                // todo: this needs to cancel any awaiting tasks
                IsConnected = await _connectionHandler.Disconnect(_connection);
                continuation.TrySetResult(IsConnected);
            }
            catch (Exception ex)
            {
                continuation.TrySetException(ex);
            }
        }
    }
}