namespace AireLogic.LyricCount.Core;

record LyricCountResult(bool ArtistFound, string? ArtistName)
{
    public static readonly LyricCountResult ArtistNotFound = new(false, null);
}

