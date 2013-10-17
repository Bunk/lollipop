using System.Threading.Tasks;
using com.riotgames.platform.harassment;
using com.riotgames.platform.summoner;

namespace Lollipop.Services
{
    public interface ISummonerService
    {
        Task<string[]> GetNamesBySummonerId(params int[] summonerIds);

        Task<string> GetInternalName(string name);

        Task<PublicSummoner> GetSummoner(string name);

        Task<AllSummonerData> GetSummonerData(int accountId);

        Task<AllPublicSummonerDataDTO> GetPublicSummonerData(int accountId);

        Task<Kudos> GetKudos(int summonerId);
    }
}