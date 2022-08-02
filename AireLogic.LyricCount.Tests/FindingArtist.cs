using AireLogic.LyricCount.MusicBrainz;

namespace AireLogic.LyricCount.Tests;

class FindingArtist
{
    [Test]
    public async Task NotFound_ShouldReturnNotFoundResult()
    {
        var handler = new LyricCountHandler(new FakeMusicBrainzClient());

        var response = await handler.GetLyricCountAsync("Unknown Artist");

        response.ArtistFound.ShouldBeFalse();
        response.ArtistName.ShouldBeNull();
    }

    [Test]
    public async Task Found_ShouldUseFirstArtist()
    {
        var handler = new LyricCountHandler(new FakeMusicBrainzClient());

        var response = await handler.GetLyricCountAsync("Search Artist");

        response.ArtistFound.ShouldBeTrue();
        response.ArtistName.ShouldBe("Found Artist 1");
    }

    [Test]
    public async Task Found_ShouldIncludeTheSongs()
    {
        var handler = new LyricCountHandler(new FakeMusicBrainzClient());

        var response = await handler.GetLyricCountAsync("Search Artist");

        response.ArtistFound.ShouldBeTrue();
        response.Songs.Select(song => song.Name)
            .ShouldBe(new[] { "Song 1", "Song 2", "Song 3" });
    }

    class FakeMusicBrainzClient : IMusicBrainzClient
    {
        static readonly ArtistResponse ArtistResponse = new()
        {
            Artists = new[] {
                new Artist { ID="11111111-1111-1111-1111-111111111111", Name="Found Artist 1" },
                new Artist { ID="22222222-2222-2222-2222-222222222222", Name="Found Artist 2" },
                new Artist { ID="33333333-3333-3333-3333-333333333333", Name="Found Artist 3" }
            }
        };

        static readonly SongsResponse Artist1SongsResponse = new()
        {
            Works = new[] {
                new Work("Song 1"),
                new Work("Song 2"),
                new Work("Song 3")
           }
        };

        static readonly ArtistResponse NotFoundResponse = new();

        IReadOnlyDictionary<string, ArtistResponse> ArtistResponses { get; } = new Dictionary<string, ArtistResponse> {
                { "Search Artist", ArtistResponse}
            };

        IReadOnlyDictionary<string, SongsResponse> SongResponses { get; } = new Dictionary<string, SongsResponse> {
                {"11111111-1111-1111-1111-111111111111", Artist1SongsResponse}
            };

        public Task<ArtistResponse> QueryArtistAsync(string artistSearch)
        {
            return Task.FromResult(ArtistResponses.GetValueOrDefault(artistSearch, NotFoundResponse));
        }

        public Task<SongsResponse> QuerySongsAsync(string artistId)
        {
            return Task.FromResult(SongResponses[artistId]);
        }
    }
}
