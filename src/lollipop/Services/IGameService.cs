using System.Threading.Tasks;
using com.riotgames.platform.game;

namespace Lollipop.Services
{
    public interface IGameService
    {
        Task<FeaturedObserverGames> FeaturedGames();

        Task<PlatformGameLifecycleDTO> GetActiveGameFor(string summonerName);

        Task<GameDTO> GetGameTimerState(long gameId, GameState state = GameState.CHAMP_SELECT);
    }
}