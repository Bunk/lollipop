using System;
using System.Runtime.Serialization;

namespace Lollipop
{
    [Serializable]
    public class LeagueException : Exception
    {
        public LeagueException()
        {
        }

        public LeagueException(string message) : base(message)
        {
        }

        public LeagueException(string message, Exception inner) : base(message, inner)
        {
        }

        protected LeagueException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }

    [Serializable]
    public class LoginFailedException : LeagueException
    {
        public LoginFailedException()
        {
        }

        public LoginFailedException(string message) : base(message)
        {
        }

        public LoginFailedException(string message, Exception inner) : base(message, inner)
        {
        }

        protected LoginFailedException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }

    [Serializable]
    public class GameNotFoundException : LeagueException
    {
        public GameNotFoundException()
        {
        }

        public GameNotFoundException(string message) : base(message)
        {
        }

        public GameNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }

        protected GameNotFoundException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }

    [Serializable]
    public class GameNotObservableException : LeagueException
    {
        public GameNotObservableException()
        {
        }

        public GameNotObservableException(string message) : base(message)
        {
        }

        public GameNotObservableException(string message, Exception inner) : base(message, inner)
        {
        }

        protected GameNotObservableException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}