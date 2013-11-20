using System.Collections.Generic;
using com.riotgames.platform.leagues;
using com.riotgames.team;

namespace com.riotgames.platform.statistics
{
    public class EndOfGameStats : VersionedObject
    {
        public int? userId;
        public int gameId;
        public int reportGameId;
        public string summonerName;

        public GameMode gameMode;
        public GameType gameType;
        public LeagueQueue queueType;
        public string myTeamStatus;

        public bool ranked;
        public bool leveledUp;
        public bool invalid;
        public bool imbalancedTeamsNoPoints;
        public bool sendStatsToTournamentProvider;

        public int gameLength; // in minutes
        public int timeUntilNextFirstWinBonus;
        public int customMsecsUntilReset;

        public int elo;
        public int eloChange;
        public int basePoints;
        public int talentPointsGained;
        public int queueBonusEarned;
        public int experienceEarned;
        public int rpEarned;
        public int ipEarned;
        public int experienceTotal;
        public int ipTotal;
        public int boostXpEarned;
        public int boostIpEarned;
        public int odinBonusIp;
        public int completionBonusPoints;
        public int loyaltyBoostIpEarned;
        public int loyaltyBoostXpEarned;
        public int customMinutesLeftToday;
        public int coOpVsAiMinutesLeftToday;
        public int coOpVsAiMsecsUntilReset;
        public int firstWinBonus;

        public int skinIndex;

        public string roomName;
        public string roomPassword;
        public GameDifficulty difficulty;

        public object newSpells;

        public TeamInfo myTeamInfo;
        public TeamInfo otherTeamInfo;
        public List<PlayerParticipantStatsSummary> teamPlayerParticipantStats;
        public List<PlayerParticipantStatsSummary> otherTeamPlayerParticipantStats;
        public List<object> pointsPenalties;

        public EndOfGameStats()
        {
            teamPlayerParticipantStats = new List<PlayerParticipantStatsSummary>();
            otherTeamPlayerParticipantStats = new List<PlayerParticipantStatsSummary>();
        }
    }
}