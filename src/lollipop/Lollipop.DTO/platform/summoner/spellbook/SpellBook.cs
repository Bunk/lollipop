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
}