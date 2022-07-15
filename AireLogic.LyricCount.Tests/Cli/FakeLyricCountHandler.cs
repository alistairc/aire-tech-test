namespace AireLogic.LyricCount.Tests.Cli;

using AireLogic.LyricCount;

class FakeLyricCountHandler : ILyricCountHandler
{
    public Task<LyricCountResult> GetLyricCountAsync(string artistSearch)
    {
        return Task.FromResult(GetLyricCount(artistSearch));
    }

    static LyricCountResult GetLyricCount(string artistSearch) {
        if (artistSearch.StartsWith("Known Artist")) return new LyricCountResult(true, "Known Artist 1");
        return new LyricCountResult(false, null);
    }
}
