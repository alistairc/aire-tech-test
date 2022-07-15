namespace AireLogic.LyricCount;

public record LyricCountResult(bool ArtistFound, string? ArtistName)
{
    public static readonly LyricCountResult ArtistNotFound = new(false, null);

    public static LyricCountResult ForArtistFound(string artistName)
    {
        return new(true, artistName);
    }
}

