using System;

namespace Lollipop.Session
{
    public class LeagueSession : ILeagueSession
    {
        private bool _disposed;

        public LeagueSession()
        {
            
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (!disposing) return;

            _disposed = true;
        }
    }
}