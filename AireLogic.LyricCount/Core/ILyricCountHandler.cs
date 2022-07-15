namespace AireLogic.LyricCount.Core;

interface ILyricCountHandler
{
    Task<LyricCountResult> GetLyricCountAsync(string artistSearch);
}
