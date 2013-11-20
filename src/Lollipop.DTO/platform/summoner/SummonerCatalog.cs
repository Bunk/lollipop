using System.Collections.Generic;

namespace com.riotgames.platform.summoner
{
    public class SummonerCatalog
    {
        public object items;

        public List<TalentGroup> talentTree;

        public List<RuneSlot> spellBookConfig;

        public SummonerCatalog()
        {
            talentTree = new List<TalentGroup>();
            spellBookConfig = new List<RuneSlot>();
        }
    }
}