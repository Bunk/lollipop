using System;

namespace com.riotgames.platform.statistics
{
    public enum PlayerStatSummaryType
    {
        CoopVsAI,
        OdinUnranked,
        Unranked,
        Unranked3x3,
        RankedSolo5x5,
        RankedTeam3x3,
        RankedTeam5x5,
        RankedPremade3x3,
        RankedPremade5x5,
        AramUnranked5x5
    }

    public class PlayerStatSummary : VersionedObject
    {
        public int userId;
        
        public int leaves;
        public int losses;
        public int wins;
        public int rating;
        public int maxRating;

        public DateTime? modifyDate;

        public string playerStatSummaryTypeString;
        public PlayerStatSummaryType playerStatSummaryType;

        public SummaryAggStats aggregatedStats;
        public string aggregatedStatsJson;
    }
}