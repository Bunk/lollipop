namespace com.riotgames.platform.leagues.pojo
{
    public class MiniSeriesDTO
    {
        public int target;
        public int wins;
        public int losses;
        public int timeLeftToPlayMillis;

        // "WNN" (win na na)
        // "LLWWN" (loss loss win win na)
        public string progress;
    }
}