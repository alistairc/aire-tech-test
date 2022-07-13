namespace AireLogic.LyricCount.Tests.Cli;

class SuccessfulRun
{
    [Test]
    public void ShouldDisplayTheArtistName()
    {
        var result = CliTestSystem.RunWithKnownArtist();
        result.StdOutText.ShouldStartWith($"Artist: {CliTestSystem.KnownArtist}");
    }
}
