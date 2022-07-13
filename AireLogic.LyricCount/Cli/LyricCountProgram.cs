namespace AireLogic.LyricCount.Cli;

class LyricCountProgram
{
    public const string InvalidUsage = "Usage: AireLogic.LyricCount <artist>";

    TextWriter StdOut { get; }

    public LyricCountProgram(TextWriter stdOut)
    {
        StdOut = stdOut;
    }

    public ExitCode Run(string[] args)
    {
        if (args.Length != 1)
        {
            StdOut.WriteLine(InvalidUsage);
            return ExitCode.InvalidArgs;
        }
        
        StdOut.WriteLine("Hello, World!");
        return ExitCode.Success;
    }
}
