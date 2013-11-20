using System.Collections.Generic;

namespace com.riotgames.platform.catalog.runes
{
    public class Rune : VersionedObject
    {
        public int itemId;
        public int tier;
        public int gameCode;
        public int duration;

        public string baseType;
        public string name;
        public string description;
        public string toolTip;
        public string imagePath;
        public int uses;

        public RuneType runeType;
        public List<ItemEffect> itemEffects;

        public Rune()
        {
            itemEffects = new List<ItemEffect>();
        }
    }
}