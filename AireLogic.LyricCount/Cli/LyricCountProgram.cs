namespace AireLogic.LyricCount.Cli;

class LyricCountProgram
{
    TextWriter StdOut { get; }

    public LyricCountProgram(TextWriter stdOut)
    {
        StdOut = stdOut;
    }

    public ExitCode Run(string[] args)
    {
        StdOut.WriteLine("Hello, World!");
        return ExitCode.Success;
    }
}
