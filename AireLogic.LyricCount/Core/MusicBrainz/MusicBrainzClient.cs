namespace AireLogic.LyricCount.Core.MusicBrainz;

using System.Net.Http;
using System.Net.Http.Json;

class MusicBrainzClient : IMusicBrainzClient
{
    MusicBrainzSettings Settings { get; }
    HttpClient HttpClient { get; }

    public MusicBrainzClient(MusicBrainzSettings settings, HttpClient httpClient)
    {
        Settings = settings;
        HttpClient = httpClient;
    }

    public ArtistResponse QueryArtist(string artistSearch)
    {
        var requestUri = Settings.ApiUri + $"/artist?query={artistSearch}";
        var userAgent = $"{Settings.ApplicationName}/{Settings.ApplicationVersion} ({Settings.ContactEmail})";

        var message = new HttpRequestMessage(HttpMethod.Get, requestUri);
        message.Headers.Add("Accept", "application/json");
        message.Headers.Add("User-Agent", userAgent);

        // all these  .GetAwaiter().GetResult(); are horrible, but it's temporary!
        //  Need to convert this whole app to async
        var responseMessage = HttpClient.SendAsync(message)
            .GetAwaiter().GetResult();
        responseMessage.EnsureSuccessStatusCode();

        var response = responseMessage.Content.ReadFromJsonAsync<ArtistResponse>()
             .GetAwaiter().GetResult();

        return response!;
    }
}
