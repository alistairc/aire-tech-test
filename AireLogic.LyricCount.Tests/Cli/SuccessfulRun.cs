namespace AireLogic.LyricCount.Tests.Cli;

class SuccessfulRun
{
    [Test]
    public async Task ShouldDisplayTheArtistName()
    {
        var result = await CliTestSystem.RunWithKnownArtistAsync();
        result.StdOutText.ShouldStartWith($"Artist: {CliTestSystem.KnownArtist}");
    }

    [Test]
    public async Task ShouldDisplayAListOfSongs()
    {
        var result = await CliTestSystem.RunWithKnownArtistAsync();
        result.StdOutText.ShouldContain($"Song: {CliTestSystem.ExampleSong1}");
        result.StdOutText.ShouldContain($"Song: {CliTestSystem.ExampleSong2}");
    }
}
