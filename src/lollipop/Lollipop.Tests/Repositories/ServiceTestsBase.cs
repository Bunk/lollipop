using Lollipop.Auth;
using Lollipop.Session;
using NUnit.Framework;

namespace Lollipop.Tests.Repositories
{
    public static class LeagueConnections
    {
        public static readonly MultiAccountLeagueConnection Current;

        static LeagueConnections()
        {
            var client1 = new LeagueClient(new LocateServerIP(), new AuthorizationService(), new LeagueConnectionHandler());
            var client2 = new LeagueClient(new LocateServerIP(), new AuthorizationService(), new LeagueConnectionHandler());
            var account1 = new LeagueAccount(client1, LeagueRegion.NorthAmerica, "BunkTester", "leaguetester1");
            var account2 = new LeagueAccount(client2, LeagueRegion.NorthAmerica, "BunkTester2", "leaguetester2");

            Current = new MultiAccountLeagueConnection()
                .AddAccount(account1)
                .AddAccount(account2);

            var errors = Current.ConnectAll();
            Assert.That(errors, Is.Empty);
        }
    }

    [TestFixture]
    public abstract class ServiceTestsBase<T> where T : class
    {
        protected T Service;

        [TestFixtureSetUp]
        public void FixtureSetUp()
        {
            Service = CreateService(LeagueConnections.Current);
        }

        protected abstract T CreateService(ILeagueConnection connection);
    }
}