using System.Collections.Generic;
using com.riotgames.platform.broadcast;
using com.riotgames.platform.game;
using com.riotgames.platform.statistics;
using com.riotgames.platform.summoner;
using com.riotgames.platform.systemstate;

namespace com.riotgames.platform.clientfacade.domain
{
    public class LoginDataPacket
    {
        public string platformId;

        public bool matchMakingEnabled;
        public bool inGhostGame;
        public bool minorShutdownEnforced;
        public bool clientHeartBeatEnabled;
        public bool minor;

        // Monies
        public double ipBalance;
        public double rpBalance;

        public double minutesUntilMidnight;
        public int minutesUntilShutdown;
        public int leaverPenaltyLevel;
        public int leaverBusterPenaltyTime;
        public int maxPracticeGameSize;
        public int restrictedChatGamesRemaining;

        public string emailStatus; // not_validated

        public SummonerCatalog summonerCatalog;
        public AllSummonerData allSummonerData;
        public PlayerStatSummaries playerStatSummaries;
        public ClientSystemStatesNotification clientSystemStates;
        public BroadcastNotification broadcastNotification;
        public List<string> languages;
        public List<GameTypeConfigDTO> gameTypeConfigs;

        public object reconnectInfo;

        public LoginDataPacket()
        {
            languages = new List<string>();
            gameTypeConfigs = new List<GameTypeConfigDTO>();
        }
    }
}