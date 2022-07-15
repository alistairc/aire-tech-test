using AireLogic.LyricCount.Cli;
using AireLogic.LyricCount.Core;
using AireLogic.LyricCount.Core.MusicBrainz;

var httpClient = new HttpClient();

var mainHandler = new LyricCountHandler(new 
    MusicBrainzClient(new MusicBrainzSettings(), httpClient)
);

var program = new LyricCountProgram(
    Console.Out,
    mainHandler
);

return (int) await program.RunAsync(args);
