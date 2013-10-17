using System.Threading.Tasks;
using com.riotgames.platform.game;

namespace Lollipop.Services
{
    public interface IGameService
    {
        Task<PlatformGameLifecycleDTO> GetActiveGameFor(string summonerName);

        Task<FeaturedObserverGames> FeaturedGames();
    }
}