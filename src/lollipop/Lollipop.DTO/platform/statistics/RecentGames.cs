using System.Collections.Generic;

namespace com.riotgames.platform.statistics
{
	public class RecentGames : VersionedObject
	{
        public int userId;
	    public string recentGamesJson;

		// Values are always null it seems
		public Dictionary<string, object> playerGameStatsMap;

		public List<PlayerGameStats> gameStatistics;
		
		public RecentGames()
		{
		    playerGameStatsMap = new Dictionary<string, object>();
		    gameStatistics = new List<PlayerGameStats>();
		}
	}
}
