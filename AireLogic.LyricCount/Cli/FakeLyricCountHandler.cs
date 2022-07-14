namespace AireLogic.LyricCount.Cli;

using AireLogic.LyricCount.Core;

class FakeLyricCountHandler : ILyricCountHandler
{
    public LyricCountResult GetLyricCount(string artistSearch)
    {
        if (artistSearch.StartsWith("Known Artist")) return new LyricCountResult(true, "Known Artist 1");
        return new LyricCountResult(false, null);
    }
}
