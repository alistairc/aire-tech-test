namespace AireLogic.LyricCount.Core.MusicBrainz;

interface IMusicBrainzClient
{
    Task<ArtistResponse> QueryArtistAsync(string artistSearch);
}
