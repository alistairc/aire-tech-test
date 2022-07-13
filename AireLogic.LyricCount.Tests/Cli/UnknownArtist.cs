using AireLogic.LyricCount.Cli;

namespace AireLogic.LyricCount.Tests.Cli;

class UnknownArtist
{
    [Test]
    public void ShouldDisplayUnknownArtistMessage()
    {
        var result = CliTestSystem.RunWithUnknownArtist();
        result.StdOutText.ShouldStartWith(LyricCountProgram.UnknownArtist);
    }

    [Test]
    public void ShouldSetNoDataExitCode()
    {
        var result = CliTestSystem.RunWithUnknownArtist();
        result.ExitCode.ShouldBe(ExitCode.NoData);
    }
}