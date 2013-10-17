using System;
using System.Net.Http;
using System.Threading.Tasks;
using com.riotgames.platform.game;
using Lollipop.Session;
using Newtonsoft.Json;

namespace Lollipop.Services
{
    public class GameService : IGameService
    {
        private readonly ILeagueConnection _conn;

        public GameService(ILeagueConnection connection)
        {
            _conn = connection;
        }

        public Task<PlatformGameLifecycleDTO> GetActiveGameFor(string summonerName)
        {
            return _conn.Call<PlatformGameLifecycleDTO>("gameService", "retrieveInProgressSpectatorGameInfo",
                                                             summonerName);
        }

        public async Task<FeaturedObserverGames> FeaturedGames()
        {
            // note: This is an HTTP call
            using (var client = new HttpClient())
            {
                var uri = new Uri(_conn.Region.SpectatorUri, "/observer-mode/rest/featured");
                var content = await client.GetStringAsync(uri);
                var response = JsonConvert.DeserializeObject<FeaturedObserverGames>(content);

                return response;
            }
        }
    }
}

//public enum LeagueErrorCode {
//    UNKNOWN,
//    UNSUPPORTED_SERVER,
//    NETWORK_ERROR,
//    RTMP_ERROR,
//    AUTHENTICATION_ERROR,
//    SUMMONER_NOT_FOUND,
//    SUMMONER_NOT_IN_LEAGUE,
//    ACTIVE_GAME_NOT_FOUND("com.riotgames.platform.game.GameNotFoundException"),
//    ACTIVE_GAME_NOT_SPECTATABLE("com.riotgames.platform.game.GameObserverModeNotEnabledException");
//    
//    private String _exceptionString;
//    
//    private LeagueErrorCode() { }
//    
//    private LeagueErrorCode(String exceptionString) {
//        _exceptionString = exceptionString;
//    }
//    
//    public String getExceptionString() {
//        return _exceptionString;
//    }
//    
//    public static LeagueErrorCode getErrorCodeForException(String exceptionString) {
//        for(LeagueErrorCode code : LeagueErrorCode.values())
//            if(code.getExceptionString() != null && code.getExceptionString().equals(exceptionString))
//                return code;
//        return RTMP_ERROR;
//    }
//}