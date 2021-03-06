﻿using System.Collections.Generic;
using System.Threading.Tasks;
using com.riotgames.platform.harassment;
using com.riotgames.platform.summoner;
using Lollipop.Session;

namespace Lollipop.Services
{
    public class SummonerService : ISummonerService
    {
        private const string Endpoint = "summonerService";

        private readonly ILeagueAccount _conn;

        public SummonerService(ILeagueAccount connection)
        {
            _conn = connection;
        }

        public async Task<string[]> NamesById(params int[] summonerIds)
        {
            var values = await _conn.Call<List<string>>(Endpoint, "getSummonerNames", summonerIds);
            return values.ToArray();
        }

        public Task<string> InternalName(string name)
        {
            return _conn.Call<string>(Endpoint, "getSummonerInternalNameByName", name);
        }

        public Task<PublicSummoner> Get(string name)
        {
            return _conn.Call<PublicSummoner>(Endpoint, "getSummonerByName", name);
        }

        public Task<AllSummonerData> Data(int accountId)
        {
            return _conn.Call<AllSummonerData>(Endpoint, "getAllSummonerDataByAccount", accountId);
        }

        public Task<AllPublicSummonerDataDTO> PublicData(int accountId)
        {
            return _conn.Call<AllPublicSummonerDataDTO>(Endpoint, "getAllPublicSummonerDataByAccount", accountId);
        }

//        public Task<SummonerRuneInventory> GetRuneInventory(int summonerId)
//        {
//            return _conn.Call<SummonerRuneInventory>("summonerRuneService", "getSummonerRuneInventory", summonerId);
//        }
//
//        public Task<SpellBookDTO> GetRunes(int summonerId)
//        {
//            return _conn.Call<SpellBookDTO>("spellBookService", "getSpellBook", summonerId);
//        }
//
//        public Task<MasteryBookDTO> GetMasteries(int summonerId)
//        {
//            return _conn.Call<MasteryBookDTO>("masteryBookService", "getMasteryBook", summonerId);
//        }

        public async Task<Kudos> Kudos(int summonerId)
        {
            var payload = string.Format("{{\"commandName\":\"{0}\",\"summonerId\":{1}}}", "TOTALS", summonerId);
            var result = await _conn.Call<LcdsResponseString>("clientFacadeService", "callKudos", payload);
            if (result == null || string.IsNullOrWhiteSpace(result.value))
                return null;

            return com.riotgames.platform.harassment.Kudos.Parse(result.value);
        }
    }
}