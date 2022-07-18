using System.Collections.Concurrent;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using AireLogic.LyricCount.MusicBrainz;

namespace AireLogic.LyricCount.Tests.MusicBrainz;

class MusicBrainzTestSystem
{
    //This is a cut-down example of a response, with only the fields we currently use
    public const string ArtistJsonResponse = @"{""artists"":[{""id"":""11111111-1111-1111-1111-111111111111"",""name"":""First Match""},{""id"": ""22222222-2222-2222-2222-222222222222"",""name"": ""Second Match""}]}";

    public const int ArtistsCount = 2;
    public const string SecondArtist = "Second Match";
    private const string DefaultSearchArtist = "Example";

    FakeHttpMessageHandler FakeHttp { get; } = new();

    public Task<ArtistResponse> GetArtist(string artistSearch = DefaultSearchArtist)
    {
        var settings = new MusicBrainzSettings
        {
            ApiUri = "https://musicbrainz.api.root",
            ApplicationName = "AppFromSettings",
            ApplicationVersion = "1.2.3",
            ContactEmail = "EmailFromSettings"
        };
        var client = new MusicBrainzClient(settings, FakeHttp.ToHttpClient());
        return client.QueryArtistAsync(artistSearch);
    }

    public Uri GetLastRequestUri()
    {
        var messages = FakeHttp.ReceivedMessages.ToArray();
        messages.ShouldNotBeEmpty();
        return messages.Last().RequestUri!;
    }

    public HttpHeadersNonValidated LastRequestHeaders()
    {
        var messages = FakeHttp.ReceivedMessages.ToArray();
        messages.ShouldNotBeEmpty();
        return messages.Last().Headers.NonValidated;
    }

    class FakeHttpMessageHandler : HttpMessageHandler
    {
        public ConcurrentQueue<HttpRequestMessage> ReceivedMessages { get; } = new();

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            ReceivedMessages.Enqueue(request);

            if (request.RequestUri!.AbsolutePath.EndsWith("artist"))
            {
                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(ArtistJsonResponse, Encoding.UTF8, "application/json")
                });
            }
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.NotFound));
        }

        public HttpClient ToHttpClient()
        {
            return new HttpClient(this);
        }
    }
}

