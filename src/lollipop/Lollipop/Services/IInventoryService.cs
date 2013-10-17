using System.Collections.Generic;
using System.Threading.Tasks;
using com.riotgames.platform.catalog.champion;
using Lollipop.Session;

namespace Lollipop.Services
{
    public interface IInventoryService
    {
        Task<List<ChampionDTO>> GetAvailableChampions();
    }

    public class InventoryService : IInventoryService
    {
        private readonly ILeagueConnection _conn;

        public InventoryService(ILeagueConnection connection)
        {
            _conn = connection;
        }

        public async Task<List<ChampionDTO>> GetAvailableChampions()
        {
            return await _conn.Call<List<ChampionDTO>>("inventoryService", "getAvailableChampions");
        }
    }
}