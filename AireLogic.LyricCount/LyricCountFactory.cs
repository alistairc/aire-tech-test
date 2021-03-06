using AireLogic.LyricCount.MusicBrainz;

namespace AireLogic.LyricCount;

//Keeps the public interface small by allowing implmentations details to stay internal
public static class LyricCountFactory
{
    public static ILyricCountHandler CreateHandler(MusicBrainzSettings settings, HttpClient httpClient)
    {
        return new LyricCountHandler(
            new MusicBrainzClient(settings, httpClient)
        );
    }
}
