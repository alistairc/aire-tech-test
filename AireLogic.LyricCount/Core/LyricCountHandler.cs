namespace AireLogic.LyricCount.Core;

using System.Threading.Tasks;
using AireLogic.LyricCount.Core.MusicBrainz;

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
        return LyricCountResult.ForArtistFound(response.Artists.First().Name);
    }
}
