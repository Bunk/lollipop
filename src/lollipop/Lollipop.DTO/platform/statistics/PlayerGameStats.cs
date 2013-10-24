using System;
using System.Collections.Generic;
using com.riotgames.platform.gameclient.domain;
using com.riotgames.platform.leagues;

namespace com.riotgames.platform.statistics
{
    public enum GameType
    {
        MATCHED_GAME,
        CUSTOM_GAME,
        COOP_VS_AI_GAME,
        RANKED_GAME,
        NORMAL_GAME
    }

    public enum GameDifficulty
    {
        NONE, 
        EASY
    }

    public class PlayerGameStats : VersionedObject
    {
        // Is always set to null now, used to be an integer
        public long? id;
        public int userId;
        public int summonerId;
        public int championId;
        public int teamId;
        public int gameId;
        public int gameMapId;

        public int level;
        public int skinIndex;
        public string skinName;
        public int spell1;
        public int spell2;

        public bool premadeTeam;
        public bool ranked;
        public bool leaver;
        public bool afk;
        public bool eligibleFirstWinOfDay;
        public bool invalid;

        public DateTime createDate;

        public string gameType;
        public GameType gameTypeEnum;
        public GameMode gameMode;
        public LeagueQueue queueType;
        public LeagueQueue subType;
        public GameDifficulty? difficulty;
        public string difficultyString;

        public int timeInQueue;
        public int ipEarned;
        public int boostIpEarned;
        public int experienceEarned;
        public int boostXpEarned;
        public int eloChange;
        public int KCoefficient;
        public int rating;
        public int teamRating;
        public int adjustedRating;
        public int userServerPing;
        public int premadeSize;

        public double predictedWinPct;

        public List<FellowPlayerInfo> fellowPlayers;
        public List<RawStat> statistics;
        public string rawStatsJson;

        public PlayerGameStats()
        {
            fellowPlayers = new List<FellowPlayerInfo>();
            statistics = new List<RawStat>();
        }
    }
}