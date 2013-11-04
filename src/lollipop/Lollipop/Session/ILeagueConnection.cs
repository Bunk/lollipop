using System;
using System.Threading.Tasks;
using com.riotgames.platform.login;

namespace Lollipop.Session
{
    public interface ILeagueConnection : IDisposable
    {
        bool IsConnected { get; }

        IRtmpConnection Connection { get; }

        LeagueConnection RouteEventsTo(object obj);

        LeagueConnection Setup(Action<IRtmpConnection> action);

        Task<bool> Connect(LeagueRegion region, AuthenticationCredentials credentials);

        bool Disconnect();
    }
}