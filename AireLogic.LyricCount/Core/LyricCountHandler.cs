namespace AireLogic.LyricCount.Core;

using AireLogic.LyricCount.Core.MusicBrainz;

class LyricCountHandler : ILyricCountHandler
{
    public LyricCountHandler(IMusicBrainzClient client)
    {
        Client = client;
    }

    IMusicBrainzClient Client { get; }

    public LyricCountResult GetLyricCount(string artistSearch)
    {
        var response = Client.QueryArtist(artistSearch);
        if (response.Artists.Count == 0) { return LyricCountResult.ArtistNotFound; }
        return LyricCountResult.ForArtistFound(response.Artists.First().Name);
    }
}
