using AireLogic.LyricCount.Cli;

var program = new LyricCountProgram(
    Console.Out,
    new FakeLyricCountHandler()
);

return (int)program.Run(args);
