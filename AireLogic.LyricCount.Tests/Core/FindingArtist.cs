namespace AireLogic.LyricCount.Tests.Core;

using AireLogic.LyricCount.Core;
using AireLogic.LyricCount.Core.MusicBrainz;

class FindingArtist
{
    [Test]
    public void NotFound_ShouldReturnNotFoundResult()
    {
        var handler = new LyricCountHandler(new FakeMusicBrainzClient());

        var response = handler.GetLyricCount("Unknown Artist");

        response.ArtistFound.ShouldBeFalse();
        response.ArtistName.ShouldBeNull();
    }

    [Test]
    public void Found_ShouldUseFirstArtist()
    {
        var mbResponse = new ArtistResponse
        {
            Artists = new[] {
                new Artist { ID="11111111-1111-1111-1111-111111111111", Name="Found Artist 1" },
                new Artist { ID="22222222-2222-2222-2222-222222222222", Name="Found Artist 2" },
                new Artist { ID="33333333-3333-3333-3333-333333333333", Name="Found Artist 3" }
            }
        };

        var musicBrainzClient = new FakeMusicBrainzClient()
        {
            Responses = new Dictionary<string, ArtistResponse> {
                { "Search Artist", mbResponse}
            }
        };

        var handler = new LyricCountHandler(musicBrainzClient);
        var response = handler.GetLyricCount("Search Artist");

        response.ArtistFound.ShouldBeTrue();
        response.ArtistName.ShouldBe("Found Artist 1");
    }

    class FakeMusicBrainzClient : IMusicBrainzClient
    {
        static readonly ArtistResponse NotFoundResponse = new();

        public IReadOnlyDictionary<string, ArtistResponse> Responses { private get; init; } = new Dictionary<string, ArtistResponse>();

        public ArtistResponse QueryArtist(string artistSearch)
        {
            return Responses.GetValueOrDefault(artistSearch, NotFoundResponse);
        }
    }
}
