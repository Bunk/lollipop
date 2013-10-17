using System.Threading.Tasks;
using Lollipop.Session;

namespace Lollipop.Auth
{
    public interface IAuthorize
    {
        Task<string> GetAuthToken(LeagueRegion region, string username, string password);
    }
}