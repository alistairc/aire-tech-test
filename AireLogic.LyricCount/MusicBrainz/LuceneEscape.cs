using System.Text;

namespace AireLogic.LyricCount.MusicBrainz;

static class LuceneEscape
{
    static readonly string[] Specials =
    {
        "+",
        "-",
        "&&",
        "||",
        "!",
        "(",
        ")",
        "{",
        "}",
        "[",
        "]",
        "^",
        "\"",
        "~",
        "*",
        "?",
        ":",
        "/"
    };

    const char EscapeChar = '\\';

    public static string Escape(string input)
    {
        var sb = new StringBuilder(input);

        sb.Replace($"{EscapeChar}", $"{EscapeChar}{EscapeChar}");
        foreach (var s in Specials)
        {
            sb.Replace(s, $"{EscapeChar}{s}");
        };

        return sb.ToString();
    }
}