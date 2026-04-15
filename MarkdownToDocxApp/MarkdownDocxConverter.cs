using DocSharp.Markdown;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace MarkdownToDocxApp;

internal static class MarkdownDocxConverter
{
    public static void Convert(string markdownPath, string outputPath, AppConfig config)
    {
        if (!File.Exists(markdownPath))
        {
            throw new FileNotFoundException("Die ausgewaehlte Markdown-Datei wurde nicht gefunden.", markdownPath);
        }

        if (File.Exists(outputPath))
        {
            File.Delete(outputPath);
        }

        var markdown = MarkdownSource.FromFile(markdownPath);
        var converter = new MarkdownConverter();
        converter.ToDocx(markdown, outputPath, WordprocessingDocumentType.Document, false);

        using var document = WordprocessingDocument.Open(outputPath, true);
        ApplyFonts(document, config);
        var mainPart = document.MainDocumentPart ?? throw new InvalidOperationException("Das DOCX-Hauptdokument konnte nicht geoeffnet werden.");
        var mainDocument = mainPart.Document ?? throw new InvalidOperationException("Das DOCX enthaelt kein Hauptdokument.");
        mainDocument.Save();
    }

    private static void ApplyFonts(WordprocessingDocument document, AppConfig config)
    {
        var mainPart = document.MainDocumentPart ?? throw new InvalidOperationException("Das DOCX-Hauptdokument konnte nicht geoeffnet werden.");
        var styleDefinitionsPart = mainPart.StyleDefinitionsPart ?? mainPart.AddNewPart<StyleDefinitionsPart>();
        styleDefinitionsPart.Styles ??= new Styles();

        EnsureDocumentDefaults(styleDefinitionsPart.Styles, config.TextFont);
        ApplyTextStyles(styleDefinitionsPart.Styles, config.TextFont);
        ApplyCodeStyles(styleDefinitionsPart.Styles, config.SourceFont);
        ApplyRunFonts(document, config.SourceFont);

        styleDefinitionsPart.Styles.Save();
    }

    private static void EnsureDocumentDefaults(Styles styles, string textFont)
    {
        styles.DocDefaults ??= new DocDefaults();
        styles.DocDefaults.RunPropertiesDefault ??= new RunPropertiesDefault();
        styles.DocDefaults.RunPropertiesDefault.RunPropertiesBaseStyle ??= new RunPropertiesBaseStyle();

        SetRunFonts(styles.DocDefaults.RunPropertiesDefault.RunPropertiesBaseStyle, textFont);
    }

    private static void ApplyTextStyles(Styles styles, string textFont)
    {
        var textStyleIds = new[]
        {
            "Normal",
            "MDParagraph",
            "MDQuote",
            "MDHeading1",
            "MDHeading2",
            "MDHeading3",
            "MDHeading4",
            "MDHeading5",
            "MDHeading6",
            "MDBulletedListItem",
            "MDOrderedListItem",
            "MDDefinitionItem",
            "MDDefinitionTerm"
        };

        foreach (var styleId in textStyleIds)
        {
            if (FindStyle(styles, styleId) is { } style)
            {
                style.StyleRunProperties ??= new StyleRunProperties();
                SetRunFonts(style.StyleRunProperties, textFont);
            }
        }
    }

    private static void ApplyCodeStyles(Styles styles, string sourceFont)
    {
        var codeStyleIds = new[]
        {
            "MDCodeBlock",
            "MDCodeInline"
        };

        foreach (var styleId in codeStyleIds)
        {
            if (FindStyle(styles, styleId) is { } style)
            {
                style.StyleRunProperties ??= new StyleRunProperties();
                SetRunFonts(style.StyleRunProperties, sourceFont);
            }
        }
    }

    private static void ApplyRunFonts(WordprocessingDocument document, string sourceFont)
    {
        var codeParagraphs = document.MainDocumentPart?
            .Document?
            .Body?
            .Descendants<Paragraph>()
            .Where(p => string.Equals(p.ParagraphProperties?.ParagraphStyleId?.Val?.Value, "MDCodeBlock", StringComparison.Ordinal))
            .ToList()
            ?? [];

        foreach (var paragraph in codeParagraphs)
        {
            foreach (var run in paragraph.Elements<Run>())
            {
                run.RunProperties ??= new RunProperties();
                SetRunFonts(run.RunProperties, sourceFont);
            }
        }

        var inlineCodeRuns = document.MainDocumentPart?
            .Document?
            .Body?
            .Descendants<Run>()
            .Where(r => string.Equals(r.RunProperties?.RunStyle?.Val?.Value, "MDCodeInline", StringComparison.Ordinal))
            .ToList()
            ?? [];

        foreach (var run in inlineCodeRuns)
        {
            run.RunProperties ??= new RunProperties();
            SetRunFonts(run.RunProperties, sourceFont);
        }
    }

    private static Style? FindStyle(Styles styles, string styleId)
    {
        return styles.Elements<Style>()
            .FirstOrDefault(s => string.Equals(s.StyleId?.Value, styleId, StringComparison.Ordinal));
    }

    private static void SetRunFonts(OpenXmlCompositeElement runPropertiesContainer, string fontName)
    {
        var runFonts = runPropertiesContainer.Elements<RunFonts>().FirstOrDefault();
        if (runFonts is null)
        {
            runFonts = new RunFonts();
            runPropertiesContainer.PrependChild(runFonts);
        }

        runFonts.Ascii = fontName;
        runFonts.HighAnsi = fontName;
        runFonts.ComplexScript = fontName;
        runFonts.EastAsia = fontName;
    }
}
