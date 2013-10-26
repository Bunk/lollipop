using System;
using System.Globalization;
using System.Reflection;
using System.Threading.Tasks;
using com.riotgames.platform.login;
using FluorineFx;
using FluorineFx.Messaging.Messages;
using FluorineFx.Net;
using log4net;
using Lollipop.Utility;

namespace Lollipop.Session
{
    public class LeagueClient : IFlashRemotingClient
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof (LeagueClient));

        private readonly IAuthorize _authorize;
        private readonly LeagueConnection _connections;
        private readonly ILocateServerIP _locateServer;
        private readonly CredentialManager _credentialManager;

        static LeagueClient()
        {
            TypeHelper.AddTargetAssembly(Assembly.GetAssembly(typeof (AuthenticationCredentials)));
        }

        public LeagueClient(ILocateServerIP locateServer, IAuthorize authorize, LeagueConnection handler)
        {
            if (locateServer == null) throw new ArgumentNullException("locateServer");
            if (authorize == null) throw new ArgumentNullException("authorize");

            _connections = handler;
            _locateServer = locateServer;
            _authorize = authorize;
            _credentialManager = new CredentialManager();
        }

        public bool IsConnected
        {
            get
            {
                return _connections != null && _connections.IsConnected;
            }
        }

        public async Task<bool> Connect(LeagueRegion region, string username, string password)
        {
            if (_connections.IsConnected)
                return true;

            _connections
                .RouteEventsTo(this)
                .Setup(conn =>
                {
                    //conn.OnConnect += (sender, args) => IsConnected = true;
                    conn.OnDisconnect += (sender, args) =>
                    {
                        if (IsConnected)
                            _connections.Disconnect();
                    };
                    conn.NetStatus += NetStatus;
                });

            var serverIp = await _locateServer.Locate(region);
            if (string.IsNullOrWhiteSpace(serverIp))
                serverIp = "127.0.0.1";

            var token = await _authorize.GetAuthToken(region, username, password);
            if (string.IsNullOrWhiteSpace(token))
                throw new LeagueConnectionException("Could not login to the server.");

            var credentials = _credentialManager.Generate(serverIp, username, password, token);
            return await _connections.Connect(region, credentials);
        }

        public bool Disconnect()
        {
            return _connections != null && _connections.Disconnect();
        }

        public Task<T> Call<T>(string service, string method, params object[] parameters)
        {
            var connection = _connections.Connection;
            if (connection == null || !connection.Connected)
                throw new InvalidOperationException("The connection is not currently open.");

            var invocation = new Invocation<T>(service, method, parameters);
            return invocation.Execute(connection);
        }

        // ReSharper disable once InconsistentNaming
        // - Note: this method is case-sensitive and is called by the NetConnection through reflection
        public void receive(AsyncMessage message)
        {
            Log.InfoFormat("RECV: {0}", message);
            // todo: Handle the different types of messages that can show up here
            // GameDTO
            //   - When in the queue to join a game
            // PlayerCredentialsDto
            //   - When a game starts, this is filled with the observer server info
            // EogPointChangeBreakdown
            //   - After a game completes
            // EndOfGameStats
            //   - After a game completes
            // StoreAccountBalanceNotification
            //   - After a game completes
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
    }
}