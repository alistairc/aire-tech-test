using AireLogic.LyricCount;
using AireLogic.LyricCount.Cli;

var httpClient = new HttpClient();

var mainHandler = LyricCountFactory.CreateHandler(new MusicBrainzSettings(), httpClient);

var program = new LyricCountProgram(
    Console.Out,
    mainHandler
);

return (int) await program.RunAsync(args);
