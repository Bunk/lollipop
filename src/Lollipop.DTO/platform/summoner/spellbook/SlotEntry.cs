using com.riotgames.platform.catalog.runes;

namespace com.riotgames.platform.summoner.spellbook
{
    public class SlotEntry : VersionedObject
    {
        public int runeId;
        public int runeSlotId;

        public Rune rune;
        public RuneSlot runeSlot;
    }
}