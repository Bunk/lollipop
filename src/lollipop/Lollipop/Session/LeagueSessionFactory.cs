namespace Lollipop.Session
{
    public class LeagueSessionFactory : ILeagueSessionFactory
    {
        public ILeagueSession Create()
        {
            return new LeagueSession();
        }
    }
}