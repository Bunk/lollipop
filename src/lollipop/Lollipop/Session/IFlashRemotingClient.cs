using System.Threading.Tasks;

namespace Lollipop.Session
{
    public interface IFlashRemotingClient
    {
        string AuthToken { get; }

        string ServerIp { get; }

        bool IsConnected { get; }

        IFlashRemotingClient Use(LeagueRegion region, string username, string password);

        Task Login();

        Task Connect();

        Task Reconnect();

        Task Disconnect();

        Task<T> Call<T>(string service, string method, params object[] parameters);
    }
}