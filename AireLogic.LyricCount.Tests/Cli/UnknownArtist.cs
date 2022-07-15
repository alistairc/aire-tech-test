using AireLogic.LyricCount.Cli;

namespace AireLogic.LyricCount.Tests.Cli;

class UnknownArtist
{
    [Test]
    public async Task ShouldDisplayUnknownArtistMessageAsync()
    {
        var result = await CliTestSystem.RunWithUnknownArtistAsync();
        result.StdOutText.ShouldStartWith(LyricCountProgram.UnknownArtist);
    }

    [Test]
    public async Task ShouldSetNoDataExitCodeAsync()
    {
        var result = await CliTestSystem.RunWithUnknownArtistAsync();
        result.ExitCode.ShouldBe(ExitCode.NoData);
    }
}