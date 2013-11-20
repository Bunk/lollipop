namespace com.riotgames.platform.broadcast
{
    public class BroadcastNotification
    {
        // todo: Figure out what these can be...
        public object broadcastMessages;
    }

    public class BroadcastAlert
    {
        public string alertMessage;
    }

    public class BroadcastMessage
    {
        public int id;

        public string content;

        public string severity;

        public string messageKey;
    }
}