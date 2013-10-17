using System;

namespace com.riotgames.platform.catalog.champion
{
    public class ChampionSkinDTO
    {
        public int championId;
        public int skinId;

        public bool owned;
        public bool lastSelected;
        public bool stillObtainable;
        public bool freeToPlayReward;

        public DateTime purchaseDate;
        public DateTime endDate;

        public int winCountRemaining;
    }
}
