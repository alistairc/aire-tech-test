namespace AireLogic.LyricCount.Cli;

class LyricCountProgram
{
    public const string InvalidUsage = "Usage: AireLogic.LyricCount.Cli <artist>";
    public const string UnknownArtist = "Artist not found";

    TextWriter StdOut { get; }
    ILyricCountHandler Handler { get; }

    public LyricCountProgram(TextWriter stdOut, ILyricCountHandler handler)
    {
        StdOut = stdOut;
        Handler = handler;
    }

    public async Task<ExitCode> RunAsync(string[] args)
    {
        if (args.Length != 1)
        {
            await StdOut.WriteLineAsync(InvalidUsage);
            return ExitCode.InvalidArgs;
        }

        var artist = args[0];
        var result = await Handler.GetLyricCountAsync(artist);

        if (!result.ArtistFound)
        {
            await StdOut.WriteLineAsync(UnknownArtist);
            return ExitCode.NoData;
        }

        await StdOut.WriteLineAsync($"Artist: {result.ArtistName}");

        foreach (var song in result.Songs)
        {
            await StdOut.WriteLineAsync($"Song: {song.Name}");
        }
        
        return ExitCode.Success;
    }
}
