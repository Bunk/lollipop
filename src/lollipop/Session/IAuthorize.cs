using System.Threading.Tasks;

namespace Lollipop.Session
{
    public interface IAuthorize
    {
        Task<string> GetAuthToken(LeagueRegion region, string username, string password);
    }
}