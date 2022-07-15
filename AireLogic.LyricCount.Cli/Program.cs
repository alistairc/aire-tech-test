using AireLogic.LyricCount.Cli;
using AireLogic.LyricCount.Core;

var httpClient = new HttpClient();

var mainHandler = LyricCountFactory.CreateHandler(new MusicBrainzSettings(), httpClient);

var program = new LyricCountProgram(
    Console.Out,
    mainHandler
);

return (int) await program.RunAsync(args);
