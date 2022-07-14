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

        var message = new HttpRequestMessage(HttpMethod.Get, requestUri);
        message.Headers.TryAddWithoutValidation("Accept", "application/json");
        message.Headers.TryAddWithoutValidation("User-Agent", "AireLogic.LyricCount/1.0 (alistairc-lyric-count@altmails.com)");

        // all these  .GetAwaiter().GetResult(); are horrible, but it's temporary!
        //  Need to convert this whole app to async
        var responseMessage = HttpClient.SendAsync(message)
            .GetAwaiter().GetResult();

        var response = responseMessage.Content.ReadFromJsonAsync<ArtistResponse>()
             .GetAwaiter().GetResult();  
        
        return response!;
    }
}
