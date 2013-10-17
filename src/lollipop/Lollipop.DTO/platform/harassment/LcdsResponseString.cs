using Newtonsoft.Json.Linq;

namespace com.riotgames.platform.harassment
{
    public class LcdsResponseString
    {
        public string value { get; set; }
    }

    public class Kudos
    {
        public int friendly { get; set; }
        public int helpful { get; set; }
        public int teamwork { get; set; }
        public int honorableOpponent { get; set; }

        public static Kudos Parse(string json)
        {
            var obj = JObject.Parse(json);
            var totals = (JArray) obj["totals"];
            var values = totals.ToObject<int[]>();
            var result = new Kudos
            {
                friendly = values[1],
                helpful = values[2],
                teamwork = values[3],
                honorableOpponent = values[4]
            };
            return result;
        }
    }
}
