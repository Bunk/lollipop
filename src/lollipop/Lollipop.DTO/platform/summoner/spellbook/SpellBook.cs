using System;
using System.Collections.Generic;

namespace com.riotgames.platform.summoner.spellbook
{
    public class SpellBook : VersionedObject
    {
        public int summonerId;
        public string dateString;
        public List<SpellBookPage> bookPages;

        public SpellBook()
        {
            bookPages = new List<SpellBookPage>();
        }
    }

    public class SpellBookDTO : VersionedObject
    {
        public int summonerId;
        public string dateString;

        public List<SpellBookPageDTO> bookPages;
        
        public string bookPagesJson;

        public SpellBookDTO()
        {
            bookPages = new List<SpellBookPageDTO>();
        }
    }

    public class SpellBookPageDTO : VersionedObject
    {
        public int summonerId;
        public int pageId;
        public string name;
        public bool current;
        public DateTime createDate;

        public List<SlotEntry> slotEntries;

        public SpellBookPageDTO()
        {
            slotEntries = new List<SlotEntry>();
        }
    }
}