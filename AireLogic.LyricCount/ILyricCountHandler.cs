namespace AireLogic.LyricCount;

public interface ILyricCountHandler
{
    Task<LyricCountResult> GetLyricCountAsync(string artistSearch);
}
