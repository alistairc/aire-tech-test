namespace AireLogic.LyricCount;

public record LyricCountResult(bool ArtistFound, string? ArtistName, IReadOnlyCollection<Song> Songs)
{
    public static readonly LyricCountResult ArtistNotFound = new(false, null, Array.Empty<Song>());

    public static LyricCountResult ForArtistFound(string artistName, IReadOnlyCollection<Song> songs)
    {
        return new(true, artistName, songs);
    }
}

