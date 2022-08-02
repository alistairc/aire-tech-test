using System.Collections.Concurrent;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using AireLogic.LyricCount.MusicBrainz;

namespace AireLogic.LyricCount.Tests.MusicBrainz;

static class MusicBrainzTestSystem
{
    public record HttpRequest(HttpHeadersNonValidated HttpHeaders, Uri Uri);

    public record ApiCallResult<TData>(TData Value, IReadOnlyCollection<HttpRequest> Requests)
    {
        public HttpRequest LastRequest => Requests.Last();
    }

    //This is a cut-down example of a response, with only the fields we currently use
    public const string ArtistJsonResponse = @"{""artists"":[{""id"":""11111111-1111-1111-1111-111111111111"",""name"":""First Match""},{""id"": ""22222222-2222-2222-2222-222222222222"",""name"": ""Second Match""}]}";
    public const string SongsJsonResponse = @"{""works"":[{""title"":""First Song""},{""title"": ""Second Song""}]}";

    public const int ArtistsCount = 2;
    public const string SecondArtist = "Second Match";
    private const string DefaultSearchArtist = "Example";

    public const int SongCount = 2;
    public const string SecondSong = "Second Song";
    public const string DefaultArtistID = "11111111-1111-1111-1111-111111111111";

    static MusicBrainzSettings Settings { get; } = new MusicBrainzSettings
    {
        ApiUri = "https://musicbrainz.api.root",
        ApplicationName = "AppFromSettings",
        ApplicationVersion = "1.2.3",
        ContactEmail = "EmailFromSettings"
    };

    //TODO: Name should be suffixed with Async
    public static async Task<ApiCallResult<ArtistResponse>> GetArtist(string artistSearch = DefaultSearchArtist)
    {
        var fakeHttp = new FakeHttpMessageHandler();
        var client = new MusicBrainzClient(Settings, fakeHttp.ToHttpClient());
        return new ApiCallResult<ArtistResponse>(
            await client.QueryArtistAsync(artistSearch),
            fakeHttp.GetRequests()
        );
    }

    public static async Task<ApiCallResult<SongsResponse>> GetSongsAsync()
    {
        var fakeHttp = new FakeHttpMessageHandler();
        var client = new MusicBrainzClient(Settings, fakeHttp.ToHttpClient());
        return new ApiCallResult<SongsResponse>(
            await client.QuerySongsAsync("11111111-1111-1111-1111-111111111111"),
            fakeHttp.GetRequests()
        );
    }
    class FakeHttpMessageHandler : HttpMessageHandler
    {
        ConcurrentQueue<HttpRequestMessage> ReceivedMessages { get; } = new();

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            ReceivedMessages.Enqueue(request);

            var uri = request.RequestUri!;

            if (uri.AbsolutePath.EndsWith("artist"))
            {
                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(ArtistJsonResponse, Encoding.UTF8, "application/json")
                });
            }
            if (uri.AbsolutePath.EndsWith("work"))
            {
                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(SongsJsonResponse, Encoding.UTF8, "application/json")
                });
            }

            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.NotFound));
        }

        public HttpClient ToHttpClient()
        {
            return new HttpClient(this);
        }

        public IReadOnlyCollection<HttpRequest> GetRequests()
        {
            return ReceivedMessages
                .Select(msg => new HttpRequest(msg.Headers.NonValidated, msg.RequestUri!))
                .ToArray();
        }
    }
}

