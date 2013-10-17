using System;
using System.Collections.Generic;

namespace com.riotgames.platform.summoner.spellbook
{
    public class SpellBookPage : VersionedObject
    {
        public int summonerId;
        public int pageId;
        public string name;
        public bool current;
        public DateTime createDate;

        public List<SlotEntry> slotEntries;
    }
}