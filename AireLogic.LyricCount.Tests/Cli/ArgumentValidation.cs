namespace AireLogic.LyricCount.Tests.Cli;

using AireLogic.LyricCount.Cli;

class ArgumentValidation
{
    [Test]
    public void WithNoArgs_ShouldPrintUsageMessage()
    {
        ShouldBeInvalid(Array.Empty<string>());
    }

    [Test]
    public void WithTwoArgs_ShouldPrintUsageMessage()
    {
        ShouldBeInvalid(new[] { "arg1", "arg2" });
    }

    [Test]
    public void WithOneArg_ShouldSucceed()
    {
        var args = new[] { "Example Artist" };
        var result = CliTestSystem.RunWithArgs(args);
        result.ExitCode.ShouldBe(ExitCode.Success);
    }

    static void ShouldBeInvalid(string[] args)
    {
        var result = CliTestSystem.RunWithArgs(args);

        result.StdOutText.ShouldBe(LyricCountProgram.InvalidUsage + Environment.NewLine);
        result.ExitCode.ShouldBe(ExitCode.InvalidArgs);
    }

    static class CliTestSystem
    {
        public record CliOutput(ExitCode ExitCode, string StdOutText);

        public static CliOutput RunWithArgs(string[] args)
        {
            var stdOut = new StringWriter();
            var program = new LyricCountProgram(stdOut);
            var exitCode = program.Run(args);

            return new CliOutput(exitCode, stdOut.ToString());
        }
    }
}

