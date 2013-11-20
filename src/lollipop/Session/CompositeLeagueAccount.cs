using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lollipop.Session
{
    /// <summary>
    /// Maintains an open queue of connections that are cycled through
    /// to request data from the League servers.  This should reduce the
    /// liklihood of any particular account being throttled due to an overload
    /// in requests.
    /// </summary>
    public class CompositeLeagueAccount : ILeagueAccount
    {
        private readonly List<ILeagueAccount> _clients;
        private int _index;

        public LeagueRegion Region { get; private set; }

        public bool IsConnected
        {
            get
            {
                return _clients.Any(c => c.IsConnected);
            }
        }

        public CompositeLeagueAccount()
        {
            _clients = new List<ILeagueAccount>();
        }
        
        public async Task Connect()
        {
            foreach (var account in _clients.Where(account => !account.IsConnected))
            {
                await account.Connect();
            }
        }

        public bool Disconnect()
        {
            var ret = false;
            foreach (var account in _clients)
            {
                ret = account.Disconnect();
            }
            return ret;
        }

        public CompositeLeagueAccount AddAccount(ILeagueAccount account)
        {
            if (Region == null)
                Region = account.Region;

            if (Region != account.Region)
                throw new LeagueException("All accounts in a single league connection must be from the same region.");

            if (!_clients.Contains(account))
                _clients.Add(account);

            return this;
        }

        public ILeagueAccount GetNextAccount()
        {
            if (_clients.Count == 0)
                throw new LeagueConnectionException("There are no accounts defined for this connection.  " +
                                                    "Please add some accounts to this connection.");

            foreach (var _ in _clients)
            {
                _index = (_index + 1)%_clients.Count;
                var account = _clients[_index];
                if (account.IsConnected)
                    return account;
            }

            throw new LeagueConnectionException("There are no currently connected accounts.");
        }

        public Dictionary<ILeagueAccount, Exception> ConnectAll()
        {
            var map = new Dictionary<ILeagueAccount, Exception>();
            var tasks = new Dictionary<ILeagueAccount, Task>();

            foreach (var account in _clients.Where(account => !account.IsConnected))
            {
                tasks[account] = account.Connect();
            }

            foreach (var task in tasks)
            {
                try
                {
                    task.Value.Wait();
                }
                catch (Exception ex)
                {
                    map[task.Key] = ex;
                }
            }

            return map;
        }

        public Task<T> Call<T>(string service, string method, params object[] parameters)
        {
            return GetNextAccount().Call<T>(service, method, parameters);
        }
    }
}