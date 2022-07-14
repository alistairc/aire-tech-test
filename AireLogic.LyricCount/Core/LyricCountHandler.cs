namespace AireLogic.LyricCount.Core;

class LyricCountHandler : ILyricCountHandler
{
    public LyricCountResult GetLyricCount(string artistSearch)
    {
        return LyricCountResult.ArtistNotFound;
    }
}