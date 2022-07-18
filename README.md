# Aire Logic Tech Test

The technical test for Aire Logic interview - Interact with APIs

    Produce a program which, when given the name of an artist, will produce the average
    (mean) number of words in their songs.

    This should be a CLI application that is usable from the command line.

## Current State

Still in the early stages, 
- Searches for an Artist on MusicBrainz and shows the closest match, or Not Found


## Build and Test

This project uses the `dotnet` CLI tooling.  To build and run the tests:

```
    dotnet test
```

You can run the app locally with this
```
    dotnet run --project AireLogic.LyricCount.Cli "Metallica"
```
See below for more details of CLI usage

To build a releasable version:

```
    dotnet publish --configuration=Release
```
The binaries are here: `/bin/Release/net6.0/publish/`


## Dependencies

This project uses .NET 6.0 and C# 10.  Installation of the .NET 6 SDK is required.  All other libraries should be restored by the build from nuget.org

Additional libraries:

Main App:
- none

Tests:
- NUnit 
- Shouldly

## CLI usage

Usage: `AireLogic.LyricCount.Cli <artist search text>`

Exit Codes
- 0 = Success
- 1 = InvalidArgs
- 2 = NoData

Examples:

```
    $ AireLogic.LyricCount.Cli "Clutch"
    Artist: Clutch
    $ echo $?
    0

    $ AireLogic.LyricCount.Cli "hgdfskjhgdsf"
    Artist not found
    $ echo $?
    2

    $ AireLogic.LyricCount.CLi too many args
    Usage: AireLogic.LyricCount.Cli <artist>
    $ echo $?
    1
```

## Limitations
- The End To End test uses the real MusicBrainz API, so needs access and is only as reliable as that API.  I think that's OK for now, but we might want to build a fake one for testing.  
- No exception handling or logs

## Next steps
- Configurability - Read settings from config - URLs and stuff
- Exception handling.  Might want something more user friendly + a log for details?
- Get the songs from MusicBrainz  
  Some complexity here, needs multiple request for prolific artists >100 'works'  
  Probably needs some kind of progress output
- Get lyrics from lyricsovh
  Obviously need to handle songs not being found  
  This is going to want some sort of concurrency.  
  Maybe only use a sample of a few songs as it's super slow?
- The actual calculation of the average
- Maybe some packaging

## Notes for reviewers

I'm building this on Linux using the dotnet CLI and VS Code.
I don't expect that to cause any issues, but thought it's worth mentioning that I'm not using Windows or Visual Studio, so
for example I won't be using the VS project templates.  I do plan to validate that it builds and looks OK in Win/VS, but haven't yet

I've used small branches with merge commits so that you can see where I'd have made a PR.
Might want to look at `git log --first-parent` to just see the big picture

General approach is to get something very simple working and tested ASAP, then evolve it.  I've tried to stick to modern .Net conventions,
and fairly standard libraries
