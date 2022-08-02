namespace AireLogic.LyricCount.Tests.MusicBrainz;

class QueryingForArtist
{
    [Test]
    public async Task ShouldReturnTheArtistResponse()
    {
        var result = await MusicBrainzTestSystem.GetArtist();

        result.Value.ShouldNotBeNull();
        result.Value.Artists.Count.ShouldBe(MusicBrainzTestSystem.ArtistsCount);
        result.Value.Artists.ElementAt(1).Name.ShouldBe(MusicBrainzTestSystem.SecondArtist);
    }

    [Test]
    public async Task ShouldSendExpectedRequestHeaders()
    {
        var result = await MusicBrainzTestSystem.GetArtist();
        var headers = result.LastRequest.HttpHeaders;

        headers["Accept"].ShouldHaveSingleItem().ShouldBe("application/json");

        //User-Agent gets split like this by HTTP client
        headers["User-Agent"].ShouldBe(new[] { "AppFromSettings/1.2.3", "(EmailFromSettings)" });
    }

    [Test]
    public async Task ShouldUrlEscapeTheArtistName()
    {
        var result = await MusicBrainzTestSystem.GetArtist("Artist With Spaces");

        var uri = result.LastRequest.Uri;
        uri.Query.ShouldContain("query=Artist+With+Spaces");
    }

    [Test]
    public async Task ShouldEscapeLuceneSpecialCharBeforeUrlEscapingArtist()
    {
        var result = await MusicBrainzTestSystem.GetArtist("AC/DC");

        var uri = result.LastRequest.Uri;
        uri.Query.ShouldContain("query=AC%5C%2FDC");
        //                                  ^ %2F = /, URL encoded
        //                               ^ %5C = \, to escape the forward slash (for lucene)
    }
}
