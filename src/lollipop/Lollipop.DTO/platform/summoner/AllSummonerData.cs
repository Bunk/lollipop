using com.riotgames.platform.summoner.spellbook;

namespace com.riotgames.platform.summoner
{
    public class AllSummonerData : VersionedObject
    {
        public SpellBook spellBook;
        public Summoner summoner;
        public SummonerDefaultSpells summonerDefaultSpells;
        public SummonerTalentsAndPoints summonerTalentsAndPoints;
        public SummonerLevel summonerLevel;
        public SummonerLevelAndPoints summonerLevelAndPoints;
    }
}