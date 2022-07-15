namespace AireLogic.LyricCount.Tests.Core.MusicBrainz;

using System.Net;
using System.Text;
using System.Threading.Tasks;
using AireLogic.LyricCount.Core;
using AireLogic.LyricCount.Core.MusicBrainz;
using RichardSzalay.MockHttp;

class QueryingForArtist
{
    //This is a cut-down example of a response, with only the fields we currently use
    const string ArtistJsonResponse = @"{""artists"":[{""id"":""11111111-1111-1111-1111-111111111111"",""name"":""First Match""},{""id"": ""22222222-2222-2222-2222-222222222222"",""name"": ""Second Match""}]}";

    [Test]
    public async Task ShouldReturnTheArtistResponseAsync()
    {
        //TODO: Look at a better way of doing these tests
        //Not really a fan of this way of verifying the headers, would prefer to assert afterwards
        //and really want a separate test per header
        //Would like to just record the incoming messages - will look at a better solution later

        var fakeHttp = new MockHttpMessageHandler();
        fakeHttp
            .When(HttpMethod.Get, "https://musicbrainz.api.root/artist?query=Example")
            .WithHeaders("Accept", "application/json")
            .WithHeaders("User-Agent",  "AppFromSettings/1.2.3")
            .WithHeaders("User-Agent", "(EmailFromSettings)")  //User-Agent gets split like this by HTTP client
            .Respond(HttpStatusCode.OK, new StringContent(ArtistJsonResponse, Encoding.UTF8, "application/json"));

        var settings = new MusicBrainzSettings
        {
            ApiUri = "https://musicbrainz.api.root",
            ApplicationName = "AppFromSettings",
            ApplicationVersion ="1.2.3",
            ContactEmail = "EmailFromSettings"
        };
        var client = new MusicBrainzClient(settings, fakeHttp.ToHttpClient());
        var result = await client.QueryArtistAsync("Example");

        result.ShouldNotBeNull();
        result.Artists.Count.ShouldBe(2);
        result.Artists.ElementAt(1).Name.ShouldBe("Second Match");
    }

    //TODO: Should escape Queries
}
