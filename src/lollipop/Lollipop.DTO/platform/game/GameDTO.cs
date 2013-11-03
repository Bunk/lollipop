using System.Collections.Generic;
using com.riotgames.platform.leagues;
using com.riotgames.platform.statistics;

namespace com.riotgames.platform.game
{
    public class GameDTO : VersionedObject
    {
        public long id;
        public int mapId;
        public int? glmGameId;
        public int gameTypeConfigId;
        public int optimisticLock;

        public bool passwordSet;

        public string glmHost;
        public int glmPort;
        public int glmSecurePort;
        public string passbackUrl;
        public object passbackDataPacket;

        public string name;
        public string roomName;
        public string roomPassword;
        public string spectatorsAllowed;
        public string statusOfParticipants; // ie, 1111111111
        public string ownerSummary;
        public string banOrder;
        public int? pickTurn;

        public int spectatorDelay;
        public int maxNumPlayers;
        public int queuePosition;
        public int expiryTime;
        public int joinTimerDuration;

        public LeagueQueue queueTypeName;
        public GameState gameState;
        public GameType gameType;
        public GameMode gameMode;
        public string gameStateString;
        public string terminatedCondition;
        public string terminatedConditionString;

        public List<PlayerChampionSelectionDTO> playerChampionSelections;
        public List<BannedChampion> bannedChampions;
        public List<Participant> teamOne;
        public object teamOnePickOutcome;
        public List<Participant> teamTwo;
        public object teamTwoPickOutcome;
        public List<object> practiceGameRewardsDisabledReasons;
        public List<object> observers;

        public GameDTO()
        {
            playerChampionSelections = new List<PlayerChampionSelectionDTO>();
            bannedChampions = new List<BannedChampion>();
            teamOne = new List<Participant>();
            teamTwo = new List<Participant>();
            practiceGameRewardsDisabledReasons = new List<object>();
            observers = new List<object>();
        }
    }
}