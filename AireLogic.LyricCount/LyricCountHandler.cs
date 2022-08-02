using AireLogic.LyricCount.MusicBrainz;

namespace AireLogic.LyricCount;

class LyricCountHandler : ILyricCountHandler
{
    public LyricCountHandler(IMusicBrainzClient client)
    {
        Client = client;
    }

    IMusicBrainzClient Client { get; }

    public async Task<LyricCountResult> GetLyricCountAsync(string artistSearch)
    {
        var response = await Client.QueryArtistAsync(artistSearch);
        if (response.Artists.Count == 0) { return LyricCountResult.ArtistNotFound; }

        var artist = response.Artists.First();
        var songsResponse = await Client.QuerySongsAsync(artist.ID);
        var songs = songsResponse.Works.Select(work => new Song(work.Title));

        return LyricCountResult.ForArtistFound(artist.Name, songs.ToArray());
    }
}
