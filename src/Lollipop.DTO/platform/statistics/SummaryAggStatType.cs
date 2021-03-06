﻿namespace com.riotgames.platform.statistics
{
    public enum RawStatType
    {
        VICTORY_POINT_TOTAL,
        WIN,
        LOSE,
        LEVEL,
        COMBAT_PLAYER_SCORE,
        OBJECTIVE_PLAYER_SCORE,
        TEAM_OBJECTIVE,
        CHAMPIONS_KILLED,
        MINIONS_KILLED,
        NEUTRAL_MINIONS_KILLED,
        NEUTRAL_MINIONS_KILLED_YOUR_JUNGLE,
        NEUTRAL_MINIONS_KILLED_ENEMY_JUNGLE,
        TURRETS_KILLED,
        BARRACKS_KILLED,
        ASSISTS,
        NUM_DEATHS,
        GOLD_EARNED,
        VISION_WARDS_BOUGHT_IN_GAME,
        SIGHT_WARDS_BOUGHT_IN_GAME,
        WARD_KILLED,
        WARD_PLACED,
        NODE_NEUTRALIZE,
        NODE_NEUTRALIZE_ASSIST,
        NODE_CAPTURE,
        NODE_CAPTURE_ASSIST,
        TRUE_DAMAGE_DEALT_PLAYER,
        TRUE_DAMAGE_DEALT_TO_CHAMPIONS,
        TRUE_DAMAGE_TAKEN,
        PHYSICAL_DAMAGE_DEALT_PLAYER,
        PHYSICAL_DAMAGE_DEALT_TO_CHAMPIONS,
        PHYSICAL_DAMAGE_TAKEN,
        MAGIC_DAMAGE_DEALT_PLAYER,
        MAGIC_DAMAGE_DEALT_TO_CHAMPIONS,
        MAGIC_DAMAGE_TAKEN,
        TOTAL_DAMAGE_DEALT,
        TOTAL_DAMAGE_DEALT_TO_CHAMPIONS,
        TOTAL_DAMAGE_TAKEN,
        TOTAL_HEAL,
        TOTAL_TIME_CROWD_CONTROL_DEALT,
        TOTAL_TIME_SPENT_DEAD,
        TOTAL_SCORE_RANK,
        TOTAL_PLAYER_SCORE,
        LARGEST_KILLING_SPREE,
        LARGEST_MULTI_KILL,
        LARGEST_CRITICAL_STRIKE,
        ITEM0,
        ITEM1,
        ITEM2,
        ITEM3,
        ITEM4,
        ITEM5
    }

    public enum SummaryAggStatType
    {
        TOTAL_MINION_KILLS,
        TOTAL_TURRETS_KILLED,
        TOTAL_NEUTRAL_MINIONS_KILLED,
        TOTAL_NODE_CAPTURE,
        TOTAL_NODE_NEUTRALIZE,
        TOTAL_ASSISTS,
        TOTAL_DEATHS_PER_SESSION,
        TOTAL_CHAMPION_KILLS,
        TOTAL_DECAYER,
        MAX_TEAM_OBJECTIVE,
        MAX_OBJECTIVE_PLAYER_SCORE,
        MAX_TOTAL_PLAYER_SCORE,
        MAX_COMBAT_PLAYER_SCORE,
        MAX_NODE_NEUTRALIZE,
        MAX_NODE_NEUTRALIZE_ASSIST,
        MAX_NODE_CAPTURE,
        MAX_NODE_CAPTURE_ASSIST,
        MAX_ASSISTS,
        MAX_NUM_DEATHS,
        MAX_CHAMPIONS_KILLED,
        AVERAGE_TEAM_OBJECTIVE,
        AVERAGE_OBJECTIVE_PLAYER_SCORE,
        AVERAGE_TOTAL_PLAYER_SCORE,
        AVERAGE_COMBAT_PLAYER_SCORE,
        AVERAGE_NODE_NEUTRALIZE,
        AVERAGE_NODE_NEUTRALIZE_ASSIST,
        AVERAGE_NODE_CAPTURE,
        AVERAGE_NODE_CAPTURE_ASSIST,
        AVERAGE_ASSISTS,
        AVERAGE_NUM_DEATHS,
        AVERAGE_CHAMPIONS_KILLED
    }
}