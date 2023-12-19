using MarkdownDocumentBuilder.Mermaid.SequenceDiagram.Elements;

namespace MarkdownDocumentBuilder.Mermaid.SequenceDiagram;

public class MermaidSequenceDiagram
{
    public IList<MermaidSequenceDiagramElement> Elements { get; } = new List<MermaidSequenceDiagramElement>();
    private readonly Action<IMermaidSequenceDiagramBuilder> _executeBuildSteps;

    internal static MermaidSequenceDiagram Init()
    {
        var document = (IMermaidSequenceDiagramBuilder sequenceDiagram) => { };
        return new MermaidSequenceDiagram(document);
    }

    public MermaidSequenceDiagram(Action<IMermaidSequenceDiagramBuilder> sequenceDiagram)
    {
        _executeBuildSteps = sequenceDiagram;
    }

    public static MermaidSequenceDiagram Create(Action<IMermaidSequenceDiagramBuilder> sequenceDiagram)
        => new(sequenceDiagram);

    public MermaidSequenceDiagram Build()
    {
        var sequenceDiagramBuilder = new MermaidSequenceDiagramBuilder();
        _executeBuildSteps(sequenceDiagramBuilder);
        return sequenceDiagramBuilder.Build();
    }
}
