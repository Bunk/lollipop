using System.Threading.Tasks;

namespace Lollipop.Session
{
    public interface IFlashRemotingClient
    {
        bool IsConnected { get; }

        Task<bool> Connect(LeagueRegion region, string username, string password);

        bool Disconnect();

        Task<T> Call<T>(string service, string method, params object[] parameters);
    }
}