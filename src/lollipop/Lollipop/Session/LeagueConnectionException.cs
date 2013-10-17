using System;
using System.Runtime.Serialization;

namespace Lollipop.Session
{
    [Serializable]
    public class LeagueConnectionException : Exception
    {
        public LeagueConnectionException()
        {
        }

        public LeagueConnectionException(string message) : base(message)
        {
        }

        public LeagueConnectionException(string message, Exception inner) : base(message, inner)
        {
        }

        protected LeagueConnectionException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}