using FluentAssertions;
using MarkdownDocumentBuilder.Model;
using MarkdownDocumentBuilder.Options;
using MarkdownDocumentBuilder.Test.Unit.TestHelpers;
using MarkdownDocumentBuilder.Test.Unit.TestModel;

namespace MarkdownDocumentBuilder.Test.Unit.Builders;

[TestClass]
public class MarkdownDocumentBuilderTests
{
    [TestMethod]
    public void Build_BuildsExpectedMarkdownDocument()
    {
        var markdownContentOptions = new MarkdownContentOptions();

        var document = MarkdownDocument.Build(document =>
        {
            document.Content(content =>
            {
                content.AddHeader1("Rick Astley - Never Gonna Give You Up");

                content.AddTable<SongDetails>(
                    new()
                    {
                        Artist = "Rick Astley",
                        Title = "Never Gonna Give You Up",
                        Album = "Whenever You Need Somebody",
                        Released = "16 November 1987"
                    },
                    new()
                    {
                        Artist = "Eduard Khil",
                        Title = "I Am So Glad I'm Finally Returning Back Home",
                        Album = "Single",
                        Released = "1966"
                    });

                content.AddImage(
                    name: "rick-astley-picture",
                    path: "./assets/images/rick-astley.png",
                    caption: "Rick Astley, a hunk of a man!");

                content.AddHeader2("Description");

                content.AddParagraph("Rick Astley's hit song where he sings about:");

                content.AddOrderedList(
                    "Never giving you up.",
                    "Never letting you down.");

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

content.AddFencedCodeblock(codeblock:
@"foreach (var note in song)
{
    note.Play();
}",
language: "C#");

                content.AddHorizontalRule();

                content.AddParagraph("Song by Rick Astley, DocumentBuilder by Bart van Keersop.");
            });
        });

        using var stream = new MemoryStream();

        // Act
        document.SaveAsync(FilePath.TestPath);

        // Assert
        string result = stream.ReadAsString();
        string filePath = FilePath.Combine("Document", "ExpectedDocument.md");
        var expectedDocument = FileReader.ReadFile(filePath);
        result.Should().Be(expectedDocument);
    }
}