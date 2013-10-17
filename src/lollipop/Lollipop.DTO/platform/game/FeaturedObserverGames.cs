using System.Collections.Generic;

namespace com.riotgames.platform.game
{
    public class FeaturedObserverGames
    {
        public int clientRefreshInterval;

        public List<ObserverGame> gameList;

        public FeaturedObserverGames()
        {
            gameList = new List<ObserverGame>();
        }
    }
}