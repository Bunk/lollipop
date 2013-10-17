using System.Threading.Tasks;
using Lollipop.Session;
using Moq;
using NUnit.Framework;

namespace Lollipop.Tests.Session
{
    [TestFixture]
    public class MultiAccountLeagueConnectionTests
    {
        [Test]
        public void AddAccount_will_set_the_region_based_on_the_first_account_added()
        {
            var client = new Mock<IFlashRemotingClient>();
            var account = new LeagueAccount(client.Object, LeagueRegion.NorthAmerica, "testUser1", "testPassword1");
            var connection = new MultiAccountLeagueConnection()
                .AddAccount(account);

            Assert.That(connection, Is.Not.Null);
            Assert.That(connection.Region, Is.EqualTo(LeagueRegion.NorthAmerica));
        }

        [Test]
        public void AddAccount_throws_when_account_region_doesnt_match_connection_region()
        {
            var client = new Mock<IFlashRemotingClient>();
            var account = new LeagueAccount(client.Object, LeagueRegion.NorthAmerica, "testUser1", "testPassword1");
            var account2 = new LeagueAccount(client.Object, LeagueRegion.Brazil, "testUser2", "testPassword2");
            var connection = new MultiAccountLeagueConnection()
                .AddAccount(account);

            Assert.Throws<LeagueException>(() => connection.AddAccount(account2));
        }

        [Test]
        public void GetNextAccount_will_throw_when_no_accounts_are_added()
        {
            var connection = new MultiAccountLeagueConnection();

            Assert.That(connection, Is.Not.Null);
            Assert.Throws<LeagueConnectionException>(() => connection.GetNextAccount());
        }

        [Test]
        public void GetNextAccount_will_throw_when_no_connections_are_open()
        {
            var client1 = new Mock<IFlashRemotingClient>();
            client1.Setup(x => x.IsConnected).Returns(false);
            var client2 = new Mock<IFlashRemotingClient>();
            client2.Setup(x => x.IsConnected).Returns(false);

            var acct1 = new LeagueAccount(client1.Object, LeagueRegion.NorthAmerica, "testUser1", "testPassword1");
            var acct2 = new LeagueAccount(client2.Object, LeagueRegion.NorthAmerica, "testUser2", "testPassword2");
            var connection = new MultiAccountLeagueConnection()
                .AddAccount(acct1)
                .AddAccount(acct2);

            Assert.That(connection, Is.Not.Null);
            Assert.Throws<LeagueConnectionException>(() => connection.GetNextAccount());
        }

        [Test]
        public void GetNextAccount_will_cycle_through_connections_cyclicly()
        {
            var client1 = new Mock<IFlashRemotingClient>();
            client1.Setup(x => x.IsConnected).Returns(true);
            var client2 = new Mock<IFlashRemotingClient>();
            client2.Setup(x => x.IsConnected).Returns(true);
            var client3 = new Mock<IFlashRemotingClient>();
            client3.Setup(x => x.IsConnected).Returns(true);

            var acct1 = new LeagueAccount(client1.Object, LeagueRegion.NorthAmerica, "testUser1", "testPassword1");
            var acct2 = new LeagueAccount(client2.Object, LeagueRegion.NorthAmerica, "testUser2", "testPassword2");
            var acct3 = new LeagueAccount(client2.Object, LeagueRegion.NorthAmerica, "testUser3", "testPassword3");
            var connection = new MultiAccountLeagueConnection()
                .AddAccount(acct1)
                .AddAccount(acct2)
                .AddAccount(acct3);

            Assert.That(connection, Is.Not.Null);
            Assert.That(connection.GetNextAccount(), Is.EqualTo(acct2));
            Assert.That(connection.GetNextAccount(), Is.EqualTo(acct3));
            Assert.That(connection.GetNextAccount(), Is.EqualTo(acct1));
            Assert.That(connection.GetNextAccount(), Is.EqualTo(acct2));
            Assert.That(connection.GetNextAccount(), Is.EqualTo(acct3));
            Assert.That(connection.GetNextAccount(), Is.EqualTo(acct1));
        }

        [Test]
        public void ConnectAll_will_succeed_if_all_clients_are_already_connected()
        {
            var acct1 = new Mock<ILeagueAccount>();
            acct1.Setup(x => x.IsConnected).Returns(true);
            var acct2 = new Mock<ILeagueAccount>();
            acct2.Setup(x => x.IsConnected).Returns(true);
            var acct3 = new Mock<ILeagueAccount>();
            acct3.Setup(x => x.IsConnected).Returns(true);

            var errors = new MultiAccountLeagueConnection()
                .AddAccount(acct1.Object)
                .AddAccount(acct2.Object)
                .AddAccount(acct3.Object)
                .ConnectAll();

            Assert.That(errors, Is.Not.Null);
            Assert.That(errors.Count, Is.EqualTo(0));
        }

        [Test]
        public void ConnectAll_will_run_successfully_if_all_clients_connect()
        {
            var acct1 = new Mock<ILeagueAccount>();
            acct1.Setup(x => x.IsConnected).Returns(false);
            acct1.Setup(x => x.Connect()).Returns(Task.Factory.StartNew(() => { }));
            var acct2 = new Mock<ILeagueAccount>();
            acct2.Setup(x => x.IsConnected).Returns(false);
            acct2.Setup(x => x.Connect()).Returns(Task.Factory.StartNew(() => { }));
            var acct3 = new Mock<ILeagueAccount>();
            acct3.Setup(x => x.IsConnected).Returns(false);
            acct3.Setup(x => x.Connect()).Returns(Task.Factory.StartNew(() => { }));

            var errors = new MultiAccountLeagueConnection()
                .AddAccount(acct1.Object)
                .AddAccount(acct2.Object)
                .AddAccount(acct3.Object)
                .ConnectAll();

            Assert.That(errors, Is.Not.Null);
            Assert.That(errors.Count, Is.EqualTo(0));
        }

        [Test]
        public void ConnectAll_will_run_for_successful_accounts_and_return_exception_for_failed()
        {
            var acct1 = new Mock<ILeagueAccount>();
            acct1.Setup(x => x.Connect())
                .Returns(Task.Factory.StartNew(() => { throw new LeagueConnectionException("Account 1"); }));
            var acct2 = new Mock<ILeagueAccount>();
            acct2.Setup(x => x.Connect())
                .Returns(Task.Factory.StartNew(() => { throw new LeagueConnectionException("Account 2"); }));
            var acct3 = new Mock<ILeagueAccount>();
            acct3.Setup(x => x.Connect()).Returns(Task.Factory.StartNew(() => { }));

            var connection = new MultiAccountLeagueConnection()
                .AddAccount(acct1.Object)
                .AddAccount(acct2.Object)
                .AddAccount(acct3.Object);
            var errors = connection.ConnectAll();

            // Need to fake that it connected successfully
            acct3.Setup(x => x.IsConnected).Returns(true);

            Assert.That(errors, Is.Not.Null);
            Assert.That(errors.Count, Is.EqualTo(2));
            Assert.That(errors[acct1.Object].InnerException.Message, Is.EqualTo("Account 1"));
            Assert.That(errors[acct2.Object].InnerException.Message, Is.EqualTo("Account 2"));
            Assert.That(connection.GetNextAccount(), Is.EqualTo(acct3.Object));
            Assert.That(connection.GetNextAccount(), Is.EqualTo(acct3.Object));
        }
    }
}