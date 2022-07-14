namespace AireLogic.LyricCount.Cli;

using AireLogic.LyricCount.Core;

class LyricCountProgram
{
    public const string InvalidUsage = "Usage: AireLogic.LyricCount <artist>";
    public const string UnknownArtist = "Artist not found";

    TextWriter StdOut { get; }
    ILyricCountHandler Handler { get; }

    public LyricCountProgram(TextWriter stdOut, ILyricCountHandler handler)
    {
        StdOut = stdOut;
        Handler = handler;
    }

    public ExitCode Run(string[] args)
    {
        if (args.Length != 1)
        {
            StdOut.WriteLine(InvalidUsage);
            return ExitCode.InvalidArgs;
        }

        var artist = args[0];
        var result = Handler.GetLyricCount(artist);

        if (!result.ArtistFound)
        {
            StdOut.WriteLine(UnknownArtist);
            return ExitCode.NoData;
        }

        StdOut.WriteLine($"Artist: {result.ArtistName}");
        return ExitCode.Success;
    }
}
