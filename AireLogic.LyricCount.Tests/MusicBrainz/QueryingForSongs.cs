namespace AireLogic.LyricCount.Tests.MusicBrainz;

class QueryingForSongs
{
    //TODO: Lots of duplication with Querying for artists....

    [Test]
    public async Task ShouldReturnTheSongsResponse()
    {
        var sut = new MusicBrainzTestSystem();
        var result = await sut.GetSongsAsync();

        result.ShouldNotBeNull();
        result.Works.Count.ShouldBe(MusicBrainzTestSystem.SongCount);
        result.Works.ElementAt(1).Title.ShouldBe(MusicBrainzTestSystem.SecondSong);
    }

    [Test]
    public async Task ShouldSendTheExpectedQuery()
    {
        var sut = new MusicBrainzTestSystem();
        _ = await sut.GetSongsAsync();
        var uri = sut.GetLastRequestUri();
        uri.Query.ShouldContain($"query=type:song%20AND%20arid:{ MusicBrainzTestSystem.DefaultArtistID }&limit=10");
    }

    [Test]
    public async Task ShouldSendExpectedRequestHeaders()
    {
        var sut = new MusicBrainzTestSystem();
        _ = await sut.GetSongsAsync();
        var headers = sut.LastRequestHeaders();

        headers["Accept"].ShouldHaveSingleItem().ShouldBe("application/json");

        //User-Agent gets split like this by HTTP client
        headers["User-Agent"].ShouldBe(new[] { "AppFromSettings/1.2.3", "(EmailFromSettings)" });
    }
}