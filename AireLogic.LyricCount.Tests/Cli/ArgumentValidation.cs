namespace AireLogic.LyricCount.Tests.Cli;

using System.Threading.Tasks;
using AireLogic.LyricCount.Cli;

class ArgumentValidation
{
    [Test]
    public async Task WithNoArgs_ShouldPrintUsageMessageAsync()
    {
        await ShouldBeInvalidAsync(Array.Empty<string>());
    }

    [Test]
    public async Task WithTwoArgs_ShouldPrintUsageMessageAsync()
    {
        await ShouldBeInvalidAsync(new[] { "arg1", "arg2" });
    }

    [Test]
    public async Task WithOneArg_ShouldSucceedAsync()
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

