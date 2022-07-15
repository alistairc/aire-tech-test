namespace AireLogic.LyricCount;

public class MusicBrainzSettings
{
    public string ApiUri { get; set; } = "https://musicbrainz.org/ws/2";
    public string ApplicationName { get; set;} = "AireLogic.LyricCount";
    public string ApplicationVersion { get;set;} = "1.0";
    public string ContactEmail { get;set;} = "alistairc-lyric-count@altmails.com";
}
