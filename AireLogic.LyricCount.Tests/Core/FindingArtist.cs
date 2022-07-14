namespace AireLogic.LyricCount.Tests.Core;

using AireLogic.LyricCount.Core;

class FindingArtist
{

    [Test]
    public void NotFound_ShouldReturnNotFoundResult()
    {
        var handler = new LyricCountHandler();

        var response = handler.GetLyricCount("Unknown Artist");

        response.ArtistFound.ShouldBeFalse();
        response.ArtistName.ShouldBeNull();
    }
}
