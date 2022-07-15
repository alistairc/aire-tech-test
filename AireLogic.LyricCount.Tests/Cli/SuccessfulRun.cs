namespace AireLogic.LyricCount.Tests.Cli;

class SuccessfulRun
{
    [Test]
    public async Task ShouldDisplayTheArtistNameAsync()
    {
        var result = await CliTestSystem.RunWithKnownArtistAsync();
        result.StdOutText.ShouldStartWith($"Artist: {CliTestSystem.KnownArtist}");
    }
}
