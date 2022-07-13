namespace AireLogic.LyricCount.Cli;

interface ILyricCountHandler
{
    LyricCountResult GetLyricCount(string artistSearch);
}
