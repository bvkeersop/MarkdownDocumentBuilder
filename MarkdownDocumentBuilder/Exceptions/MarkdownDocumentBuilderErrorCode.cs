namespace MarkdownDocumentBuilder.Exceptions
{
    public enum MarkdownDocumentBuilderErrorCode
    {
        Unknown,
        CouldNotFindColumnAtIndex,
        CouldNotFindTableRowAtIndex,
        ProvidedEnumerableIsEmpty,
        ProvidedGenericTypeForTableDoesNotEqualRunTimeType,
    }
}
