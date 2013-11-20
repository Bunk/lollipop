using com.riotgames.platform.catalog.runes;

namespace com.riotgames.platform.catalog
{
    public class Effect : VersionedObject
    {
        public int effectId;
        public int categoryId;
        public string gameCode;
        public string name;

        public RuneType runeType;
    }
}