namespace MarkdownDocumentBuilder.Mermaid.SequenceDiagram;

internal class MermaidSequenceDiagramBuilder : IMermaidSequenceDiagramBuilder
{
    private readonly MermaidSequenceDiagram _sequenceDiagram = MermaidSequenceDiagram.Init();

    public IMermaidSequenceDiagramBuilder AddParticipant(string participant, string alias)
    {
        return this;
    }

    public IMermaidSequenceDiagramBuilder Call(string callingParticipant, string receivingParticipant, string? description = null)
        => CallInternal(Arrow.Call, callingParticipant, receivingParticipant, description);

    private IMermaidSequenceDiagramBuilder CallInternal(Arrow arrow, string callingParticipant, string receivingParticipant, string? description = null)
    {
        return this;
    }

    public MermaidSequenceDiagram Build()
    {
        throw new NotImplementedException();
    }

    public IMermaidSequenceDiagramBuilder Return(string receivingParticipant, string callingParticipant, string? description = null)
    {
        throw new NotImplementedException();
    }

    public IMermaidSequenceDiagramBuilder Loop(Action<IMermaidSequenceDiagramBuilder> loop)
    {
        throw new NotImplementedException();
    }
}
