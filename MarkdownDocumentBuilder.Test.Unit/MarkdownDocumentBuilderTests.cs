using FluentAssertions;
using MarkdownDocumentBuilder.Model;
using MarkdownDocumentBuilder.Test.Unit.Model;
using MarkdownDocumentBuilder.Test.Unit.TestHelpers;

namespace MarkdownDocumentBuilder.Test.Unit;

[TestClass]
public class MarkdownDocumentBuilderTests
{
    [TestMethod]
    public void Build_BuildsExpectedMarkdownDocument()
    {
        var document = MarkdownDocument.Build(document =>
        {
            document.Content(content =>
            {
                content.AddHeader1("Rick Astley - Never Gonna Give You Up");

                content.AddTable(new SongDetails
                {
                    Artist = "Rick Astley",
                    Title = "Never Gonna Give You Up",
                    Album = "Whenever You Need Somebody",
                    Released = "16 November 1987"
                });

                content.AddHeader2("Description");

                content.AddParagraph("Rick Astley's hit song where he sings about:");

                content.AddOrderedList("Never giving you up.", "Never letting you down.");

                content.AddBlockquote("NOTE: He will also never run around and desert you!");

                content.AddHeader2("Lyrics");

                content.AddHeader3("Verse 1");

                content.AddParagraph(new string[]
                {
                    "We're no strangers to love",
                    "You know the rules and so do I (do I)",
                    "A full commitment's what I'm thinking of",
                    "You wouldn't get this from any other guy"
                });

                content.AddHorizontalRule();

                content.AddParagraph("Song by Rick Astley, DocumentBuilder by Bart van Keersop.");
            });
        });

        using var stream = new MemoryStream();
        document.SaveAsync(stream);
        string result = stream.ReadAsString();
        string filePath = Path.Combine("Resources", "ExpectedDocument.md");
        var expectedDocument = FileReader.ReadFile(filePath);
        result.Should().Be(expectedDocument);
    }
}