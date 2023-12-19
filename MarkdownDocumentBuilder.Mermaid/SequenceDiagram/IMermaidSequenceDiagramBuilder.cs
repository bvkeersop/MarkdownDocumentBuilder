namespace MarkdownDocumentBuilder.Mermaid.SequenceDiagram;

public interface IMermaidSequenceDiagramBuilder
{
    IMermaidSequenceDiagramBuilder AddParticipant(string participant, string alias);
    IMermaidSequenceDiagramBuilder Call(string callingParticipant, string receivingParticipant, string? description = null);
    IMermaidSequenceDiagramBuilder Return(string receivingParticipant, string callingParticipant, string? description = null);
    IMermaidSequenceDiagramBuilder Loop(Action<IMermaidSequenceDiagramBuilder> loop);
    MermaidSequenceDiagram Build();
}
