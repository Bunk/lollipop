using System.Collections.Generic;
using com.riotgames.platform.statistics;

namespace com.riotgames.platform.game
{
    public class ObserverGame : VersionedObject
    {
        public long gameId;
        public int mapId;
        public int gameQueueConfigId;
        public int gameTypeConfigId;
        public string platformId;

        public GameMode gameMode;
        public GameType gameType;

        public long gameStartTime; // believe this is ticks since epoch
        public long gameLength; // not sure what this is measure in... minutes?
        
        public List<BannedChampion> bannedChampions;
        public List<ObserverPlayerParticipant> participants;

        public ObserverGame()
        {
            bannedChampions = new List<BannedChampion>();
            participants = new List<ObserverPlayerParticipant>();
        }
    }
}