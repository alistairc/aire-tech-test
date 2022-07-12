# Aire Logic Tech Test

The technical test for Aire Logic interview - Interact with APIs

    Produce a program which, when given the name of an artist, will produce the average
    (mean) number of words in their songs.

    This should be a CLI application that is usable from the command line.


## Building

Build instructions to go here, expect it to look like this:

This project uses the `dotnet` CLI tooling.  To build:

```bash
    dotnet build
```

## Running Tests

Test instructions to go here, expect it to look like this:

```bash
    dotnet test
```

## Dependencies

Dependencies to go here.  I would expect everything to be nuget based, and restored automatically

## CLI usage

Document commandline usage, expected output, exit code, likely errors

Anticipated usage will be something like:

```bash
    $ SongLength "Clutch"
    123
    $ echo $?
    0
```

## Notes for reviewers

I'm building this on Linux using the dotnet CLI and VS Code, because that's what I have on my Laptop. 
I don't expect that to cause any issues, but thought it's worth mentioning that I'm not using Windows or Visual Studio, so
for example I won't be using the VS project templates.  I do plan to validate that it builds and looks OK in Win/VS.

I'll try and include the rationale behind what I'm doing in the commit history.  I'm going to use small branches with merge commits
so that you can see where I'd have made a PR.

General approach will be to get something very simple working and tested ASAP, then evolve it.  I'll stick to to modern .Net conventions,
and fairly standard libraries
