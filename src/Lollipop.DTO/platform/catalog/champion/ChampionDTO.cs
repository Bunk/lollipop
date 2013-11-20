using System;
using System.Collections.Generic;

namespace com.riotgames.platform.catalog.champion
{
    public class ChampionDTO
    {
        public int championId;

        public bool owned;
        public bool active;
        public bool botEnabled;
        public bool rankedPlayEnabled;
        public bool freeToPlay;
        public bool freeToPlayReward;

        public DateTime purchased;
        public DateTime purchasedDate;
        public DateTime endDate;

        public int winCountRemaining;

        public List<ChampionSkinDTO> championSkins;

        public ChampionDTO()
        {
            championSkins = new List<ChampionSkinDTO>();
        }
    }
}