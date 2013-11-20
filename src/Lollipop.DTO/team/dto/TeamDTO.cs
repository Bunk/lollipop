using System;
using com.riotgames.team.stats;

namespace com.riotgames.team.dto
{
    public class TeamDTO
    {
        public TeamId teamId;

        public string name;
        public string tag;
        public string status;

        public DateTime? lastJoinDate;
        public DateTime? lastGameDate;
        public DateTime createDate;
        public DateTime modifyDate;

        public TeamStatSummary teamStatSummary;
    }
}