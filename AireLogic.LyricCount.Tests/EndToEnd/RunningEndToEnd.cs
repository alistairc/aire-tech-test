namespace AireLogic.LyricCount.Tests.EndToEnd;

using System.Diagnostics;

class RunningEndToEnd
{
    [Test]
    public void ShouldRunSuccessfully()
    {
        const string validArgs = @"""Example Artist""";
        
        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "AireLogic.LyricCount",
                Arguments = validArgs,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            }
        };
        try
        {
            process.Start();
            process.WaitForExit(10000);

            process.HasExited.ShouldBeTrue();

            var errorOutput = process.StandardError.ReadToEnd();

            process.ExitCode.ShouldBe(0, errorOutput);
            process.StandardOutput.ReadToEnd().ShouldBe("Hello, World!" + Environment.NewLine, errorOutput);
        }
        finally
        {
            process.Kill();
        }
    }
}