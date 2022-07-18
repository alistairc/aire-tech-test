namespace AireLogic.LyricCount.Tests.MusicBrainz;

class QueryingForArtist
{
    [Test]
    public async Task ShouldReturnTheArtistResponse()
    {
        var sut = new MusicBrainzTestSystem();
        var result = await sut.GetArtist();

        result.ShouldNotBeNull();
        result.Artists.Count.ShouldBe(MusicBrainzTestSystem.ArtistsCount);
        result.Artists.ElementAt(1).Name.ShouldBe(MusicBrainzTestSystem.SecondArtist);
    }

    [Test]
    public async Task ShouldSendExpectedRequestHeaders()
    {
        var sut = new MusicBrainzTestSystem();
        _ = await sut.GetArtist();
        var headers = sut.LastRequestHeaders();

        headers["Accept"].ShouldHaveSingleItem().ShouldBe("application/json");

        //User-Agent gets split like this by HTTP client
        headers["User-Agent"].ShouldBe(new[] { "AppFromSettings/1.2.3", "(EmailFromSettings)" });
    }

    [Test]
    public async Task ShouldUrlEscapeTheArtistName()
    {
        var sut = new MusicBrainzTestSystem();
        _ = await sut.GetArtist("Artist With Spaces");
        var uri = sut.GetLastRequestUri();
        uri.Query.ShouldContain("query=Artist+With+Spaces");
    }
}

