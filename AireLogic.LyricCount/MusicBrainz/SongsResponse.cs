namespace AireLogic.LyricCount.MusicBrainz;

class SongsResponse
{
    public IReadOnlyCollection<Work> Works { get; init; } = Array.Empty<Work>();
}