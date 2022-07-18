namespace AireLogic.LyricCount.Tests.Cli;

class FakeLyricCountHandler : ILyricCountHandler
{
    public Task<LyricCountResult> GetLyricCountAsync(string artistSearch)
    {
        return Task.FromResult(GetLyricCount(artistSearch));
    }

    static LyricCountResult GetLyricCount(string artistSearch)
    {
        if (artistSearch.StartsWith("Known Artist"))
        {
            return LyricCountResult.ForArtistFound(
                "Known Artist 1",
                new[] {
                    new Song("Song 1"),
                    new Song("Song 2")
                }
            );
        }
        return LyricCountResult.ArtistNotFound;
    }
}
