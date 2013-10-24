using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.riotgames.platform.matchmaking
{
    public class SearchingForMatchNotification
    {
        public List<object> playerJoinFailures;
        public List<object> ghostGameSummoners;
        public List<object> joinedQueues;
    }

    public class QueueDTO
    {
        public int queueId;
        public int waitTime;
        public int queueLength;
    }
}
