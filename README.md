# MarkdownDocumentBuilder

![Workflows: dotnet](https://github.com/bvkeersop/MarkdownDocumentBuilder/actions/workflows/pipeline.yml/badge.svg)
![GitHub](https://img.shields.io/github/license/bvkeersop/MarkdownDocumentBuilder)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=bvkeersop_MarkdownDocumentBuilder&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=bvkeersop_MarkdownDocumentBuilder)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=bvkeersop_MarkdownDocumentBuilder&metric=coverage)](https://sonarcloud.io/summary/new_code?id=bvkeersop_MarkdownDocumentBuilder)

`MarkdownDocumentBuilder` is a library that enables you to create markdown documents using a fluent syntax. The syntax is influences by [QuestPdf](https://github.com/QuestPDF/QuestPDF).

# Table of Contents

- [MarkdownDocumentBuilder](#documentbuilder)
- [Table of Contents](#table-of-contents)
  - [Creating a markdown document](#creating-a-document)
  - [Configure rendering options (optional)](#configure-rendering-options-(optional))
  - [Saving a markdown document](#saving-a-markdown-document)
  - [Supported Elements](#supported-elements)
    - [Simple](#simple)
    - [Tables](#tables)
    - [Lists](#lists)
  - [Credits](#credits)
  - [Future work](#future-work)

## Creating a markdown document

To create a markdown document, simply call the static `MarkdownDocument.Build` method. From there you can provide a lambda to build your document.
You can add markdown elements to the documents `Content` property by providing another lambda.

```C#

var markdownDocument = MarkdownDocument.Build(document =>
{
    document.Content(content =>
    {
        content.AddHeader1("Hello World!");
        content.AddParagraph("FooBar");
    });
});

```

## Configure rendering options (optional)

You can configure how your markdown document and it's content are rendered. You can provide (optional) option objects as follows, sensible defaults are used:

```C#

var markdownDocumentOptions = new MarkdownDocumentOptions();
var markdownContentOptions = new MarkdownContentOptions();

var markdownDocument = MarkdownDocument.Build(document =>
{
    document.Content(content =>
    {
        content.AddHeader1("Hello World!");
        content.AddParagraph("FooBar");
    }, 
    markdownContentOptions)

}, 
markdownDocumentOptions);

```

### MarkdownDocumentOptions

| Option                    | Type                    | Description                                                  | DefaultValue         |
| ------------------------- | ----------------------- |------------------------------------------------------------- | -------------------- |
| NewLineProvider           | INewLineProvider        | A provider for the new line symbol                           | Environment.NewLine  |
| IndentationProvider       | IIndentationProvider    | A provider for the indentations                              | 4 Spaces             |

You can use the `INewLineProviderFactory` to create a `INewLineProvider` or `IIndentationProviderFactory` to create a `IIndentationProvider`.
You can also create your own providers by implementing the corresponding interface.

> **LineEndings**: *Environment, Windows, Linux*
> **IndentationType**: *Tabs, Spaces*


## Saving a markdown document

Once you have build a document you are satisfied with, simply call the `Save` method on the returned document. The `Save` method supports writing to a `Stream` as well as to a `Path`.

```C#

// Save to a stream
using var stream = new MemoryStream();
await document.SaveAsync(stream);

// Or save to a Path
var filePath = "./Users/Rick/Lyrics.md";
await document.SaveAsync(filePath);

```
## Supported Elements

MarkdownDocumentBuilder supports adding different kinds of elements to a document. think of headers, paragraphs, tables etc. 

### Simple

To see all elements that are supported, take a look at the [IMarkdownContentBuilder](https://github.com/bvkeersop/MarkdownDocumentBuilder/blob/main/MarkdownDocumentBuilder/Builders/IMarkdownContentBuilder.cs) interface, or use intellisense while building your document.
Most elements are very straight forward and based on string parameters, there are a couple of advanced ones explained below.

### Tables

`MarkdownDocumentBuilder` supports the creation of tables by creating a POCO (Plain Old C# Object). 
It will use the name of the property as the column name and will order the columns as defined on the POCO. 

### Creating a Table

#### 1. Define a POCO

```C#

class SongDetails
{
    public string Artist { get; set; }
    public string Title { get; set; }
    public string Album { get; set; }
    public string Released { get; set; }
}

```

#### 2. Call AddTable

```C#

var document = MarkdownDocument.Build(document =>
{
    document.Content(content =>
    {
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
    }
}

```

This will render the following output:

```markdown

| Artist      | Title                                        | Album                      | Released         |
| ----------- | -------------------------------------------- | -------------------------- | ---------------- |
| Rick Astley | Never Gonna Give You Up                      | Whenever You Need Somebody | 16 November 1987 |
| Eduard Khil | I Am So Glad I'm Finally Returning Back Home | Single                     | 1966             |

```

> NOTE: In case of using an object, the values written to the table cell will be the object's `ToString()` method.

#### 3. Attribute configuration

You can annotate your POCO properties with the `Column` attribute to configure the name and the order. 
Annotating with an `IgnoreColumn` will skip rendering the column.

``` C#

class SongDetails
{
    [Column(name: "Name", order: 1)] // Overwrite the column name, and set the order of the column
    public string Artist { get; set; }

    [Column(nameof(Description))] // Not specifying an order will default the value to int.Max
    public string Title { get; set; }

    [IgnoreColumn] // Ignores the column when rendering
    public string Album { get; set; }

    [Column(alignment: Alignment.Center)] // Applies github style markdown to align the column
    public string Released { get; set; }
}

```

#### MarkdownTableOptions

There's options available to configure how a table is generated.

| Option                  | Type           | Description                                  | DefaultValue |
| ----------------------- | ---------------|--------------------------------------------- | ------------ |
| Formatting              | Formatting     | How to align the table                       | AlignColumns |
| BoldColumnNames         | bool           | Wheter to have the column names in bold text | false        |
| DefaultAlignment        | Alignment      | What default (github) alignment to use       | None         |

> **Formatting**: *AlignColumns, None*
> **Alignment**: *None, Left, Right, Center*

You can configure these on the `MarkdownTableOptions` property on the `MarkdownContentOptions`.


### Lists

`MarkdownDocumentBuilder` supports the creation of ordered and unordered lists by using simple strings.

```C#

var document = MarkdownDocument.Build(document =>
{
    document.Content(content =>
    {
        content.AddOrderedList(
            "Never giving you up.",
            "Never letting you down.");
    }
}

```

This will render the following:

```markdown

1. Never giving you up.
2. Never letting you down.

```

`MarkdownDocumentBuilder` also supports the creation of more complex ordered and unordered lists by using a POCO (Plain Old C# Object).
> NOTE: You cannot use `Structs`.

```C#

var complexList = new ComplexList()
{
    FirstItem = "FirstItem",
    NestedObject = new()
    {
        FirstNestedItem = "FirstNestedItem",
        NestedList = new List<ListItem>
        {
            new ListItem()
            {
                FirstNestedListItem = "FirstNestedListItem",
                SecondNestedListItem = "SecondNestedListItem"
            }
        },
        ThirdNestedItem = "ThirdNestedItem",
    },
    ThirdItem = "ThirdItem"
};

var markdownDocument = MarkdownDocument.Build(document =>
{
    document.Content(content =>
    {
        content.AddOrderedList(complexList, complexList);
    });
});

```

This will render the following:

```markdown

1. FirstItem
    1. FirstNestedItem
        1. FirstNestedListItem
        2. SecondNestedListItem
    2. ThirdNestedItem
2. ThirdItem

```

## Credits

`MarkdownDocumentBuilder` is inspired by [QuestPdf](https://github.com/QuestPDF/QuestPDF) and uses a similar syntax to create documents.

`MarkdownDocumentBuilder` is made possible by the following projects:

- [FluentAssertions](https://fluentassertions.com/)
- [NSubstitute](https://nsubstitute.github.io/)

## Future work

If there are any features that you would like to see implemented, please create an issue with the `enhancement` label on the [Github Issues](https://github.com/bvkeersop/MarkdownDocumentBuilder/issues) page. 
Note that I am working on this project in my free time, and might not have time to implement your request (or simply decline it since I don't see the added value for the project).