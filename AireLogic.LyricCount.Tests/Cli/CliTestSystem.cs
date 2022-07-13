namespace AireLogic.LyricCount.Tests.Cli;

using AireLogic.LyricCount.Cli;

static class CliTestSystem
{
    public record CliOutput(ExitCode ExitCode, string StdOutText);

    public const string KnownArtist = "Known Artist 1";
    public const string KnownArtistSearchString = "Known Artist";

    public static CliOutput RunWithArgs(params string[] args)
    {
        var stdOut = new StringWriter();
        var program = new LyricCountProgram(stdOut);
        var exitCode = program.Run(args);

        return new CliOutput(exitCode, stdOut.ToString());
    }

    public static CliOutput RunWithUnknownArtist()
    {
        return RunWithArgs("Unknown Artist");
    }

    public static CliOutput RunWithKnownArtist()
    {
        return RunWithArgs(KnownArtistSearchString);
    }
}
