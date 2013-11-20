using System.Collections.Generic;

namespace com.riotgames.platform.systemstate
{
    public class ClientSystemStatesNotification
    {
        public bool practiceGameEnabled;

        public bool advancedTutorialEnabled;

        public bool observerModeEnabled;

        public bool archivedStatsEnabled;

        public int clientHeartBeatRateSeconds;

        public int minNumPlayersForPracticeGame;

        public bool socialIntegrationEnabled;

        public bool tribunalEnabled;

        public List<object> observableGameModes;

        public List<int> practiceGameTypeConfigIdList;

        public List<int> freeToPlayChampionIdList;

        public List<int> inactiveChampionIdList;

        public List<int> obtainableChampionSkinIDList;
 
        public List<int> unobtainableChampionSkinIDList;

        public ClientSystemStatesNotification()
        {
            observableGameModes = new List<object>();
            practiceGameTypeConfigIdList = new List<int>();
            freeToPlayChampionIdList = new List<int>();
            inactiveChampionIdList = new List<int>();
            obtainableChampionSkinIDList = new List<int>();
            unobtainableChampionSkinIDList = new List<int>();
        }
    }
}