using System.Collections.Generic;

namespace com.riotgames.platform.summoner
{
    public class SummonerDefaultSpells : VersionedObject
    {
        public int summonerId;

        //This is still broken, it doesn't get mapped properly and results in an empty dictionary unless the object type is used
        //public Dictionary<string, object> summonerDefaultSpellMap;
        public Dictionary<string, SummonerGameModeSpells> summonerDefaultSpellMap;

        public string summonerDefaultSpellsJson;

        public SummonerDefaultSpells()
        {
            summonerDefaultSpellMap = new Dictionary<string, SummonerGameModeSpells>();
        }
    }
}