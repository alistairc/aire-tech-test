namespace AireLogic.LyricCount.Core.MusicBrainz;

using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

class MusicBrainzClient : IMusicBrainzClient
{
    MusicBrainzSettings Settings { get; }
    HttpClient HttpClient { get; }

    public MusicBrainzClient(MusicBrainzSettings settings, HttpClient httpClient)
    {
        Settings = settings;
        HttpClient = httpClient;
    }

    public async Task<ArtistResponse> QueryArtistAsync(string artistSearch)
    {
        var requestUri = Settings.ApiUri + $"/artist?query={artistSearch}";
        var userAgent = $"{Settings.ApplicationName}/{Settings.ApplicationVersion} ({Settings.ContactEmail})";

        var message = new HttpRequestMessage(HttpMethod.Get, requestUri);
        message.Headers.Add("Accept", "application/json");
        message.Headers.Add("User-Agent", userAgent);

        var responseMessage = await HttpClient.SendAsync(message);
        responseMessage.EnsureSuccessStatusCode();

        var response = await responseMessage.Content.ReadFromJsonAsync<ArtistResponse>();

        return response!;
    }
}
