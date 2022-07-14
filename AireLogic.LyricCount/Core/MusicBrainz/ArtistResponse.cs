namespace AireLogic.LyricCount.Core.MusicBrainz;

class ArtistResponse
{
    public IReadOnlyCollection<Artist> Artists { get; init; } = Array.Empty<Artist>();
}
