namespace AireLogic.LyricCount.Core;

interface ILyricCountHandler
{
    LyricCountResult GetLyricCount(string artistSearch);
}
