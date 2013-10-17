using System.Threading.Tasks;

namespace Lollipop.Session
{
    public interface ILeagueAccount
    {
        LeagueRegion Region { get; }

        bool IsConnected { get; }

        Task Connect();

        Task Disconnect();

        Task<T> Call<T>(string service, string method, params object[] args);
    }
}