namespace MarkdownDocumentBuilder.Model.Elements.Lists;

internal class NestedIndex
{
    private readonly int _startIndex;
    private int _currentLevel = 0;
    private readonly Dictionary<int, int> _nestLevelAndIndex = new();

    public NestedIndex(int startIndex = 0)
    {
        _nestLevelAndIndex.Add(0, startIndex);
        _startIndex = startIndex;
    }

    public int Get() => _nestLevelAndIndex[_currentLevel];

    public void Increment() => _nestLevelAndIndex[_currentLevel]++;

    public void Nest()
    {
        if (!_nestLevelAndIndex.ContainsKey(_currentLevel + 1))
        {
            _nestLevelAndIndex.Add(_currentLevel + 1, _startIndex);
        }

        _currentLevel++;
    }

    public void Denest()
    {
        if (!_nestLevelAndIndex.ContainsKey(_currentLevel - 1))
        {
            throw new InvalidOperationException($"Index at level {_currentLevel - 1} does not exist");
        }

        _currentLevel--;
    }
}
