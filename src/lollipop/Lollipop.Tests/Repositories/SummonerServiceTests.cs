using Lollipop.Services;
using Lollipop.Session;
using NUnit.Framework;

namespace Lollipop.Tests.Repositories
{
    [TestFixture]
    [Category("Integration")]
    public class SummonerServiceTests : ServiceTestsBase<ISummonerService>
    {
        protected override ISummonerService CreateService(ILeagueAccount connection)
        {
            return new SummonerService(connection);
        }

        [Test]
        public async void Can_pull_internal_name()
        {
            var name = await Service.InternalName("ThatsBunk");

            Assert.That(name, Is.Not.Null);
            Assert.That(name, Is.EqualTo("sum23567702"));
        }

        [Test]
        [Ignore("This method should probably be removed from the API")]
        public async void Can_pull_data()
        {
            var data = await Service.Data(37685970);

            Assert.That(data, Is.Not.Null);
            Assert.That(data.summoner.name, Is.EqualTo("ThatsBunk"));
            Assert.That(data.summoner.internalName, Is.EqualTo("thatsbunk"));
            Assert.That(data.summoner.acctId, Is.EqualTo(37685970));
            Assert.That(data.summoner.sumId, Is.EqualTo(23567702));
        }

        [Test]
        public async void Can_pull_public_data()
        {
            var data = await Service.PublicData(37685970);

            Assert.That(data, Is.Not.Null);
            Assert.That(data.summoner.name, Is.EqualTo("ThatsBunk"));
            Assert.That(data.summoner.internalName, Is.EqualTo("thatsbunk"));
            Assert.That(data.summoner.acctId, Is.EqualTo(37685970));
            Assert.That(data.summoner.sumId, Is.EqualTo(23567702));
        }

        [Test]
        public async void Can_pull_summoner_by_name()
        {
            var summoner = await Service.Get("ThatsBunk");

            Assert.That(summoner, Is.Not.Null);
            Assert.That(summoner.acctId, Is.EqualTo(37685970));
            Assert.That(summoner.summonerId, Is.EqualTo(23567702));
        }

        [Test]
        public async void Can_pull_summoner_names()
        {
            var names = await Service.NamesById(23567702, 37309352, 31137721, 23272082);

            Assert.That(names, Is.Not.Null);
            Assert.That(names.Length, Is.EqualTo(4));
            Assert.That(names[0], Is.EqualTo("ThatsBunk"));
            Assert.That(names[1], Is.EqualTo("Trust the CHOPS"));
            Assert.That(names[2], Is.EqualTo("Elca"));
            Assert.That(names[3], Is.EqualTo("KamachiTakeda"));
        }

        [Test]
        public async void Can_pull_kudos()
        {
            var kudos = await Service.Kudos(23567702);

            Assert.That(kudos, Is.Not.Null);
            Assert.That(kudos.friendly, Is.GreaterThan(0));
            Assert.That(kudos.helpful, Is.GreaterThan(0));
            Assert.That(kudos.teamwork, Is.GreaterThan(0));
            Assert.That(kudos.honorableOpponent, Is.GreaterThan(0));
        }
    }
}