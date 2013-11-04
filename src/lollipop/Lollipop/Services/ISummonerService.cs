using System.Threading.Tasks;
using com.riotgames.platform.harassment;
using com.riotgames.platform.summoner;

namespace Lollipop.Services
{
    public interface ISummonerService
    {
        Task<string[]> NamesById(params int[] summonerIds);

        Task<string> InternalName(string name);

        Task<PublicSummoner> Get(string name);

        Task<AllSummonerData> Data(int accountId);

        Task<AllPublicSummonerDataDTO> PublicData(int accountId);

        Task<Kudos> Kudos(int summonerId);
    }
}