﻿using MarkdownDocumentBuilder.Factories;
using MarkdownDocumentBuilder.Options.Enums;
using MarkdownDocumentBuilder.Utilities;

namespace MarkdownDocumentBuilder.Options;

public class MarkdownDocumentOptions
{
    /// <summary>
    /// A provider for the new line character
    /// </summary>
    public INewLineProvider NewLineProvider { get; set; } = NewLineProviderFactory.Create(LineEndings.Environment);

    /// <summary>
    /// A provider for indentation
    /// </summary>
    public IIndentationProvider IndentationProvider { get; set; } = IndentationProviderFactory.Create(IndentationType.Spaces, 4);
}
