namespace AireLogic.LyricCount.MusicBrainz;

interface IMusicBrainzClient
{
    Task<ArtistResponse> QueryArtistAsync(string artistSearch);
}
