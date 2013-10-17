using System.Threading.Tasks;

namespace Lollipop.Session
{
    /// <summary>
    /// Represents a logged-in user and is responsible for communications with the League of 
    /// Legends servers.
    /// </summary>
    public class LeagueAccount : ILeagueAccount
    {
        private readonly IFlashRemotingClient _client;
        private readonly string _username;
        private readonly string _password;

        public LeagueAccount(IFlashRemotingClient client, LeagueRegion region, string username, string password)
        {
            Region = region;

            _username = username;
            _password = password;

            _client = client;
            _client.Use(Region, _username, _password);
        }

        public LeagueRegion Region { get; private set; }

        public bool IsConnected
        {
            get { return _client.IsConnected; }
        }

        public async Task Connect()
        {
            if (_client.IsConnected)
                return;

            _client.Use(Region, _username, _password);
            await _client.Login();
            await _client.Connect();
        }

        public Task Disconnect()
        {
            return _client.Disconnect();
        }

        public Task<T> Call<T>(string service, string method, params object[] args)
        {
            return _client.Call<T>(service, method, args);
        }
    }
}