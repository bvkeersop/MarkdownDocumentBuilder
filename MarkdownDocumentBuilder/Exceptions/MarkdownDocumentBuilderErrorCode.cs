namespace DocumentBuilder.Exceptions
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
