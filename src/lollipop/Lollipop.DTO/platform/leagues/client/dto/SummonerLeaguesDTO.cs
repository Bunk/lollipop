﻿using System.Collections.Generic;
using com.riotgames.platform.leagues.pojo;

namespace com.riotgames.platform.leagues.client.dto
{
    public class SummonerLeaguesDTO
    {
        public List<LeagueListDTO> summonerLeagues;

        public SummonerLeaguesDTO()
        {
            summonerLeagues = new List<LeagueListDTO>();
        }
    }
}
