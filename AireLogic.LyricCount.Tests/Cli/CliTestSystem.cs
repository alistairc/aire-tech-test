using AireLogic.LyricCount.Cli;

namespace AireLogic.LyricCount.Tests.Cli;

static class CliTestSystem
{
    public record CliOutput(ExitCode ExitCode, string StdOutText);

    public const string KnownArtist = "Known Artist 1";
    public const string KnownArtistSearchString = "Known Artist";

    public const string ExampleSong1 = "Song 1";
    public const string ExampleSong2 = "Song 2";

    public static async Task<CliOutput> RunWithArgsAsync(params string[] args)
    {
        var stdOut = new StringWriter();
        var program = new LyricCountProgram(stdOut, new FakeLyricCountHandler());
        var exitCode = await program.RunAsync(args);

        return new CliOutput(exitCode, stdOut.ToString());
    }

    public static Task<CliOutput> RunWithUnknownArtistAsync()
    {
        return RunWithArgsAsync("Unknown Artist");
    }

    public static Task<CliOutput> RunWithKnownArtistAsync()
    {
        return RunWithArgsAsync(KnownArtistSearchString);
    }

    class FakeLyricCountHandler : ILyricCountHandler
    {
        public Task<LyricCountResult> GetLyricCountAsync(string artistSearch)
        {
            return Task.FromResult(GetLyricCount(artistSearch));
        }

        static LyricCountResult GetLyricCount(string artistSearch)
        {
            if (artistSearch.StartsWith(KnownArtistSearchString))
            {
                return LyricCountResult.ForArtistFound(
                    KnownArtist,
                    new[] {
                        new Song(ExampleSong1),
                        new Song(ExampleSong2)
                    }
                );
            }
            return LyricCountResult.ArtistNotFound;
        }
    }
}
