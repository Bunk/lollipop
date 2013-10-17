using System.Collections.Generic;

namespace com.riotgames.platform.statistics
{
    public class PlayerParticipantStatsSummary
    {
        public int teamId;
        public int userId;
        public int gameId;
        public int profileIconId;
        public int spell1Id;
        public int spell2Id;

        public string summonerName;

        public int wins;
        public int losses;
        public int leaves;
        public int elo;
        public int eloChange;
        public int level;

        public bool botPlayer;
        public bool leaver;

        public List<RawStatDTO> statistics;

        public PlayerParticipantStatsSummary()
        {
            statistics = new List<RawStatDTO>();
        }
    }
}