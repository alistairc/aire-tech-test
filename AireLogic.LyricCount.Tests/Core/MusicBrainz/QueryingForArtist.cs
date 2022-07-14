namespace AireLogic.LyricCount.Tests.Core.MusicBrainz;

using System.Net;
using System.Text;
using AireLogic.LyricCount.Core.MusicBrainz;
using RichardSzalay.MockHttp;

class QueryingForArtist
{
    //This is a cut-down example of a response, with only the fields we currently use
    const string ArtistJsonResponse = @"{""artists"":[{""id"":""11111111-1111-1111-1111-111111111111"",""name"":""First Match""},{""id"": ""22222222-2222-2222-2222-222222222222"",""name"": ""Second Match""}]}";

    [Test]
    public void ShouldReturnTheArtistResponse()
    {
        var fakeHttp = new MockHttpMessageHandler();
        fakeHttp
            .When(HttpMethod.Get, "https://musicbrainz.api.root/artist?query=Example").WithHeaders("Accept", "application/json")
            .Respond(HttpStatusCode.OK, new StringContent(ArtistJsonResponse, Encoding.UTF8, "application/json"));

        var settings = new MusicBrainzSettings
        {
            ApiUri = "https://musicbrainz.api.root"
        };
        var client = new MusicBrainzClient(settings, fakeHttp.ToHttpClient());
        var result = client.QueryArtist("Example");

        result.ShouldNotBeNull();
        result.Artists.Count.ShouldBe(2);
        result.Artists.ElementAt(1).Name.ShouldBe("Second Match");
    }

    //TODO: Should escape Queries
}
