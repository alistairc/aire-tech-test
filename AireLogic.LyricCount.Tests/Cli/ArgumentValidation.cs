using AireLogic.LyricCount.Cli;

namespace AireLogic.LyricCount.Tests.Cli;

class ArgumentValidation
{
    [Test]
    public async Task WithNoArgs_ShouldPrintUsageMessage()
    {
        await ShouldBeInvalidAsync(Array.Empty<string>());
    }

    [Test]
    public async Task WithTwoArgs_ShouldPrintUsageMessage()
    {
        await ShouldBeInvalidAsync(new[] { "arg1", "arg2" });
    }

    [Test]
    public async Task WithOneArg_ShouldSucceed()
    {
        var args = new[] { CliTestSystem.KnownArtistSearchString };
        var result = await CliTestSystem.RunWithArgsAsync(args);
        result.ExitCode.ShouldBe(ExitCode.Success);
    }

    static async Task ShouldBeInvalidAsync(string[] args)
    {
        var result = await CliTestSystem.RunWithArgsAsync(args);

        result.StdOutText.ShouldBe(LyricCountProgram.InvalidUsage + Environment.NewLine);
        result.ExitCode.ShouldBe(ExitCode.InvalidArgs);
    }
}

