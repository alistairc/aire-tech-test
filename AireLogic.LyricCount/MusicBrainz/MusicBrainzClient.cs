using System.Net.Http.Json;
using System.Web;

namespace AireLogic.LyricCount.MusicBrainz;

class MusicBrainzClient : IMusicBrainzClient
{
    MusicBrainzSettings Settings { get; }
    HttpClient HttpClient { get; }

    public MusicBrainzClient(MusicBrainzSettings settings, HttpClient httpClient)
    {
        Settings = settings;
        HttpClient = httpClient;
    }

    public Task<ArtistResponse> QueryArtistAsync(string artistSearch)
    {
        var escapedAndEncoded = HttpUtility.UrlEncode(LuceneEscape.Escape(artistSearch));
        var requestUri = Settings.ApiUri + $"/artist?query={escapedAndEncoded}";

        return GetAsync<ArtistResponse>(requestUri);
    }


    public Task<SongsResponse> QuerySongsAsync(string artistId)
    {
        var requestUri = Settings.ApiUri + $"/work?query=type:song%20AND%20arid:{ artistId }&limit=10";

        return GetAsync<SongsResponse>(requestUri);
    }

    async Task<TResponse> GetAsync<TResponse>(string requestUri)
    {
        var userAgent = $"{Settings.ApplicationName}/{Settings.ApplicationVersion} ({Settings.ContactEmail})";

        var message = new HttpRequestMessage(HttpMethod.Get, requestUri);
        message.Headers.Add("Accept", "application/json");
        message.Headers.Add("User-Agent", userAgent);

        var responseMessage = await HttpClient.SendAsync(message);
        responseMessage.EnsureSuccessStatusCode();

        var response = await responseMessage.Content.ReadFromJsonAsync<TResponse>();

        return response!;
    }
}
