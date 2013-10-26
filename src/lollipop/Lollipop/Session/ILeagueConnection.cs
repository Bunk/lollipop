using System;
using System.Threading.Tasks;
using com.riotgames.platform.login;
using FluorineFx.Net;

namespace Lollipop.Session
{
    public interface ILeagueConnection : IDisposable
    {
        bool IsConnected { get; }

        NetConnection Connection { get; }

        LeagueConnection RouteEventsTo(object obj);

        LeagueConnection Setup(Action<NetConnection> action);

        Task<bool> Connect(LeagueRegion region, AuthenticationCredentials credentials);

        bool Disconnect();
    }
}