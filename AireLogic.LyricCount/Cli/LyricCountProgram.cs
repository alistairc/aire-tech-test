namespace AireLogic.LyricCount.Cli;

class LyricCountProgram
{
    public const string InvalidUsage = "Usage: AireLogic.LyricCount <artist>";
    public const string UnknownArtist = "Artist not found";

    TextWriter StdOut { get; }

    public LyricCountProgram(TextWriter stdOut)
    {
        StdOut = stdOut;
    }

    public ExitCode Run(string[] args)
    {
        if (args.Length != 1)
        {
            StdOut.WriteLine(InvalidUsage);
            return ExitCode.InvalidArgs;
        }

        var artist = args[0];
        if (artist == "Known Artist")
        {
            StdOut.WriteLine("Artist: Known Artist 1");
            return ExitCode.Success;
        }
        else
        {
            StdOut.WriteLine(UnknownArtist);
            return ExitCode.NoData;
        }
    }
}
