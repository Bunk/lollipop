using System.Collections.Generic;
using com.riotgames.platform.leagues;
using com.riotgames.platform.summoner;

namespace com.riotgames.leagues.pojo
{
    public class LeagueListDTO
    {
        public string name;
        // this seems to be the context of the league (player's name for solo queue, team name for teams)
        public string requestorsName;
        public LeagueRank requestorsRank;
        public LeagueQueue queue;
        public LeagueTier tier;

        public List<LeagueItemDTO> entries;

        public LeagueListDTO()
        {
            entries = new List<LeagueItemDTO>();
        }
    }
}