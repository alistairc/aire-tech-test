namespace AireLogic.LyricCount.Tests.EndToEnd;

using System.Diagnostics;

class RunningEndToEnd
{
    [Test]
    public void ShouldRunSuccessfully()
    {
        const string validArgs = @"""Known Artist""";
        
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

            var standardOutput = process.StandardOutput.ReadToEnd();
            var errorOutput = process.StandardError.ReadToEnd();

            process.ExitCode.ShouldBe(0, errorOutput);
            standardOutput.ShouldStartWith("Artist:", Case.Insensitive, errorOutput);
        }
        finally
        {
            process.Kill();
        }
    }
}