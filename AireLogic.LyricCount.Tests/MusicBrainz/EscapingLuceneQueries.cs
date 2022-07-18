using AireLogic.LyricCount.MusicBrainz;

namespace AireLogic.LyricCount.Tests.MusicBrainz;

class EscapingLuceneQueries
{
    [Test]
    public void ShouldPassThroughWhenNoSpecialCharacters()
    {
        LuceneEscape.Escape("hello world").ShouldBe("hello world");
    }

    [TestCase("+")]
    [TestCase("-")]
    [TestCase("&&")]
    [TestCase("||")]
    [TestCase("!")]
    [TestCase("(")]
    [TestCase(")")]
    [TestCase("{")]
    [TestCase("}")]
    [TestCase("[")]
    [TestCase("]")]
    [TestCase("^")]
    [TestCase("\"")]
    [TestCase("~")]
    [TestCase("*")]
    [TestCase("?")]
    [TestCase(":")]
    [TestCase("/")]
    [TestCase("\\")]
    public void ShouldEscapeSpecialCharsWithABackslash(string special)
    {
        LuceneEscape.Escape($"hello{special}world")
            .ShouldBe($"hello\\{special}world");
    }

    [TestCase("&")]
    [TestCase("|")]
    [TestCase(" ")]
    public void ShouldNotEscapeThese(string notSpecial)
    {
        LuceneEscape.Escape($"hello{notSpecial}world")
            .ShouldBe($"hello{notSpecial}world");
    }
}
