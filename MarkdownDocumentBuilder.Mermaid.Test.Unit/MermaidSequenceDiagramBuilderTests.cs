using MarkdownDocumentBuilder.Mermaid.SequenceDiagram;

namespace MarkdownDocumentBuilder.Mermaid.Test.Unit;

[TestClass]
public class MermaidSequenceDiagramBuilderTests
{
    [TestMethod]
    public void Build_BuildsMermaidSequenceDiagram()
    {
        // Arrange

        // Act
        MermaidSequenceDiagram.Create(sequenceDiagram =>
        {
            sequenceDiagram.AddParticipant("R", "Rick");
            sequenceDiagram.Loop
        });
    }
}