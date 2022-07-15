namespace AireLogic.LyricCount.Core;

public interface ILyricCountHandler
{
    Task<LyricCountResult> GetLyricCountAsync(string artistSearch);
}
