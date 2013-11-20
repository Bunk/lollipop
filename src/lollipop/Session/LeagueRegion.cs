using System;

namespace Lollipop.Session
{
    public class LeagueRegion
    {
        public static LeagueRegion NorthAmerica = new LeagueRegion("NA", "North America",
                                                                   "https://lq.na1.lol.riotgames.com",
                                                                   "http://spectator.na.lol.riotgames.com",
                                                                   "rtmps://prod.na1.lol.riotgames.com:2099/");

        public static LeagueRegion EuropeWest = new LeagueRegion("EUW", "Europe West",
                                                                 "https://lq.eu.lol.riotgames.com",
                                                                 "http://spectator.eu.lol.riotgames.com",
                                                                 "rtmps://prod.eu.lol.riotgames.com:2099/");

        public static LeagueRegion EuropeNordicAndEast = new LeagueRegion("EUNE", "Europe Nordic & East",
                                                                          "https://lq.eun1.lol.riotgames.com/",
                                                                          "http://spectator.eun1.lol.riotgames.com",
                                                                          "rtmps://prod.eun1.lol.riotgames.com:2099/");

        public static LeagueRegion Brazil = new LeagueRegion("BR", "Brazil",
                                                             "https://lq.br.lol.riotgames.com",
                                                             "http://spectator.br.lol.riotgames.com",
                                                             "rtmps://prod.br.lol.riotgames.com:2099/");

        public static LeagueRegion Korea = new LeagueRegion("KR", "Korea",
                                                            "https://lq.kr.lol.riotgames.com",
                                                            "http://spectator.kr.lol.riotgames.com",
                                                            "rtmps://prod.kr.lol.riotgames.com:2099/");

        public static LeagueRegion LatinAmericaNorth = new LeagueRegion("LAN", "Latin America North",
                                                                        "https://lq.la1.lol.riotgames.com",
                                                                        "http://spectator.la1.lol.riotgames.com",
                                                                        "rtmps://prod.la1.lol.riotgames.com:2099/");

        public static LeagueRegion LatinAmericaSouth = new LeagueRegion("LAS", "Latin America South",
                                                                        "https://lq.la2.lol.riotgames.com",
                                                                        "http://spectator.la2.lol.riotgames.com",
                                                                        "rtmps://prod.la2.lol.riotgames.com:2099/");

        public static LeagueRegion Oceania = new LeagueRegion("OCE", "Oceania",
                                                              "https://lq.oc1.lol.riotgames.com",
                                                              "http://spectator.oc1.lol.riotgames.com",
                                                              "rtmps://prod.oc1.lol.riotgames.com:2099/");

        public static LeagueRegion PublicBetaEnvironment = new LeagueRegion("PBE", "Public Beta Environment",
                                                                            "https://lq.pbe1.lol.riotgames.com",
                                                                            "http://spectator.pbe1.lol.riotgames.com",
                                                                            "rtmps://prod.pbe1.lol.riotgames.com:2099/");

        // GARENA REALMS
        //region.equals("SG") || region.equals("MY") || region.equals("SG/MY"))
        //			this.server = "prod.lol.garenanow.com";
        //			this.loginQueue = "https://lq.lol.garenanow.com/";
        //			this.useGarena = true;
        //		else if (region.equals("TW"))
        //			this.server = "prodtw.lol.garenanow.com";
        //			this.loginQueue = "https://loginqueuetw.lol.garenanow.com/";
        //			this.useGarena = true;
        //		else if (region.equals("TH"))
        //			this.server = "prodth.lol.garenanow.com";
        //			this.loginQueue = "https://lqth.lol.garenanow.com/";
        //			this.useGarena = true;
        //		else if (region.equals("PH"))
        //			this.server = "prodph.lol.garenanow.com";
        //			this.loginQueue = "https://storeph.lol.garenanow.com/";
        //			this.useGarena = true;
        //		else if (region.equals("VN"))
        //			this.server = "prodvn.lol.garenanow.com";
        //			this.loginQueue = "https://lqvn.lol.garenanow.com/";
        //			this.useGarena = true;

        private LeagueRegion(string code, string name, string queueUrl, string spectatorUri, string serverUrl)
        {
            if (code == null) throw new ArgumentNullException("code");
            if (name == null) throw new ArgumentNullException("name");

            Code = code;
            Name = name;

            SpectatorUri = new Uri(spectatorUri, UriKind.Absolute);
            IpLookupUri = new Uri("http://ll.leagueoflegends.com/services/connection_info", UriKind.Absolute);
            QueueUri = new Uri(queueUrl, UriKind.Absolute);
            ServerUri = new Uri(serverUrl, UriKind.Absolute);
        }

        //todo: Garena
        //todo: Public Beta Realms

        public string Code { get; private set; }

        public string Name { get; private set; }

        public Uri IpLookupUri { get; private set; }

        public Uri ServerUri { get; private set; }

        public Uri QueueUri { get; private set; }

        public Uri SpectatorUri { get; private set; }
    }
}