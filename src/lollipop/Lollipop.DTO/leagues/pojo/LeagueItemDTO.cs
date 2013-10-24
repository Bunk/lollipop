using com.riotgames.platform.leagues;
using com.riotgames.platform.summoner;

namespace com.riotgames.leagues.pojo
{
    public class LeagueItemDTO
    {
        public LeagueQueue queueType;
        public LeagueTier tier;
        public LeagueRank rank;

        public bool hotStreak;
        public bool veteran;
        public bool inactive;
        public bool freshBlood;

        public string playerOrTeamId;
        public string playerOrTeamName;
        public string leagueName;

        public int losses;
        public int wins;
        public int leaguePoints;
        public int previousDayLeaguePosition;

        /// <summary>
        /// Time last played in milliseconds
        /// </summary>
        public double? lastPlayed;
        public double? timeLastDecayMessageShown;
        public double? timeUntilDecay;

        public MiniSeriesDTO miniSeries;
    }
}