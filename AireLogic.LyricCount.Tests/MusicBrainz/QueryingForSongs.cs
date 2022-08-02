namespace AireLogic.LyricCount.Tests.MusicBrainz;

class QueryingForSongs
{
    //TODO: Lots of duplication with Querying for artists....

    [Test]
    public async Task ShouldReturnTheSongsResponse()
    {
        var result = await MusicBrainzTestSystem.GetSongsAsync();

        result.Value.ShouldNotBeNull();
        result.Value.Works.Count.ShouldBe(MusicBrainzTestSystem.SongCount);
        result.Value.Works.ElementAt(1).Title.ShouldBe(MusicBrainzTestSystem.SecondSong);
    }

    [Test]
    public async Task ShouldSendTheExpectedQuery()
    {
        var result = await MusicBrainzTestSystem.GetSongsAsync();

        var uri = result.LastRequest.Uri;
        uri.Query.ShouldContain($"query=type:song%20AND%20arid:{ MusicBrainzTestSystem.DefaultArtistID }&limit=10");
    }

    [Test]
    public async Task ShouldSendExpectedRequestHeaders()
    {
        var result = await MusicBrainzTestSystem.GetSongsAsync();

        var headers = result.LastRequest.HttpHeaders;
        headers["Accept"].ShouldHaveSingleItem().ShouldBe("application/json");

        //User-Agent gets split like this by HTTP client
        headers["User-Agent"].ShouldBe(new[] { "AppFromSettings/1.2.3", "(EmailFromSettings)" });
    }
}
