namespace AireLogic.LyricCount.Core.MusicBrainz;

interface IMusicBrainzClient
{
    ArtistResponse QueryArtist(string artistSearch);
}
