using System;
using System.IO;
using Aspose.Diagram;

public class HeaderSettings
{
    public string HeaderLeft { get; set; }
    public string HeaderCenter { get; set; }
    public string HeaderRight { get; set; }
    public string FooterLeft { get; set; }
    public string FooterCenter { get; set; }
    public string FooterRight { get; set; }
    public double HeaderMargin { get; set; }   // inches from page edge
    public double FooterMargin { get; set; }   // inches from page edge
    public string FontName { get; set; }
    public int FontWeight { get; set; }        // 400 = normal, 700 = bold
    public int FontHeight { get; set; }        // negative value per Aspose.Diagram spec
    public BOOL FontItalic { get; set; }
    public BOOL FontUnderline { get; set; }
}

public static class DiagramHelper
{
    /// <summary>
    /// Loads a Visio diagram, applies header/footer settings, and saves the result.
    /// </summary>
    /// <param name="diagramPath">Path to the source diagram file.</param>
    /// <param name="settings">Header and footer configuration.</param>
    /// <param name="outputPath">Path where the modified diagram will be saved.</param>
    public static void ApplyHeaderAndSave(string diagramPath, HeaderSettings settings, string outputPath)
    {
        // Guard: ensure source file exists.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        // Guard: ensure output directory exists (create if missing).
        string outDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outDir) && !Directory.Exists(outDir))
        {
            Directory.CreateDirectory(outDir);
        }

        try
        {
            // Load the diagram from the specified file.
            Diagram diagram = new Diagram(diagramPath);

            // Apply header text (use empty string if null).
            diagram.HeaderFooter.HeaderLeft   = settings.HeaderLeft   ?? string.Empty;
            diagram.HeaderFooter.HeaderCenter = settings.HeaderCenter ?? string.Empty;
            diagram.HeaderFooter.HeaderRight  = settings.HeaderRight  ?? string.Empty;

            // Apply footer text (use empty string if null).
            diagram.HeaderFooter.FooterLeft   = settings.FooterLeft   ?? string.Empty;
            diagram.HeaderFooter.FooterCenter = settings.FooterCenter ?? string.Empty;
            diagram.HeaderFooter.FooterRight  = settings.FooterRight  ?? string.Empty;

            // Apply margin values (in inches).
            diagram.HeaderFooter.HeaderMargin.Value = settings.HeaderMargin;
            diagram.HeaderFooter.FooterMargin.Value = settings.FooterMargin;

            // Configure font attributes for header/footer.
            var font = diagram.HeaderFooter.HeaderFooterFont;
            font.FaceName = settings.FontName ?? "Arial";
            font.Weight   = settings.FontWeight;
            font.Height   = settings.FontHeight;
            font.Italic   = settings.FontItalic;
            font.Underline = settings.FontUnderline;

            // Save the modified diagram in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Expect at least two arguments: input diagram path and output path.
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: <program> <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath  = args[0];
        string outputPath = args[1];

        // Create a sample HeaderSettings instance (customize as needed).
        var settings = new HeaderSettings
        {
            HeaderLeft   = "Company Confidential",
            HeaderCenter = "Report Title",
            HeaderRight  = "Date: &d",
            FooterLeft   = "Prepared by: John Doe",
            FooterCenter = "Page: &p of &P",
            FooterRight  = "© 2024",
            HeaderMargin = 0.5,
            FooterMargin = 0.5,
            FontName     = "Calibri",
            FontWeight   = 700,
            FontHeight   = -16,          // corresponds to 12pt (approx.)
            FontItalic   = BOOL.False,
            FontUnderline = BOOL.False
        };

        // Apply header/footer settings and save the diagram.
        DiagramHelper.ApplyHeaderAndSave(inputPath, settings, outputPath);
    }
}