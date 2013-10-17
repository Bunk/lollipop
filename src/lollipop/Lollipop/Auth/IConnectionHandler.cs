using System.Threading;
using System.Threading.Tasks;
using com.riotgames.platform.login;
using FluorineFx.Net;

namespace Lollipop.Auth
{
    public interface IConnectionHandler
    {
        bool IsConnected { get; }

        com.riotgames.platform.login.Session Session { get; }

        CancellationTokenSource Heartbeat { get; }

        Task<bool> Connect(NetConnection connection, AuthenticationCredentials credentials);

        Task<bool> Disconnect(NetConnection connection);
    }
}