namespace MarkdownDocumentBuilder.Model.Elements.Lists;

internal class NestedIndex
{
    private readonly int _startIndex;
    private readonly Dictionary<int, int> _nestedLevelsAndIndices;
    private int _currentLevel;

    public NestedIndex(int startIndex = 0)
    {
        _startIndex = startIndex;
        _nestedLevelsAndIndices = new Dictionary<int, int>
        {
            { 0, _startIndex }
        };
    }

    public int Get() => _nestedLevelsAndIndices[_currentLevel];

    public void Reset()
    {
        foreach (var nestLevelAndIndex in _nestedLevelsAndIndices)
        {
            if (nestLevelAndIndex.Key > 0)
            {
                _nestedLevelsAndIndices[nestLevelAndIndex.Key] = _startIndex;
            }
        }
    }

    public void Increment() => _nestedLevelsAndIndices[_currentLevel]++;

    public void Nest()
    {
        if (!_nestedLevelsAndIndices.ContainsKey(_currentLevel + 1))
        {
            _nestedLevelsAndIndices.Add(_currentLevel + 1, _startIndex);
        }

        _currentLevel++;
    }

    public void Denest()
    {
        if (!_nestedLevelsAndIndices.ContainsKey(_currentLevel - 1))
        {
            throw new InvalidOperationException($"Index at level {_currentLevel - 1} does not exist");
        }

        _currentLevel--;
    }
}
