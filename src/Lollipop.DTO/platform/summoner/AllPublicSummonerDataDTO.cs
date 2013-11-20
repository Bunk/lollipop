using com.riotgames.platform.summoner.spellbook;

namespace com.riotgames.platform.summoner
{
    public class AllPublicSummonerDataDTO : VersionedObject
    {
        public SpellBookDTO spellBook;
        public BasePublicSummonerDTO summoner;
        public SummonerDefaultSpells summonerDefaultSpells;
        public SummonerTalentsAndPoints summonerTalentsAndPoints;
        public SummonerLevel summonerLevel;
        public SummonerLevelAndPoints summonerLevelAndPoints;
    }
}