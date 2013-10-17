using System.Threading.Tasks;

namespace Lollipop.Session
{
    public interface ILeagueConnection
    {
        LeagueRegion Region { get; }

        Task<T> Call<T>(string service, string method, params object[] parameters);
    }
}