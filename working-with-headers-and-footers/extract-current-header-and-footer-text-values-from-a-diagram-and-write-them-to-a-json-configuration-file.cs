using System;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputJsonPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram.
            Diagram diagram = new Diagram(inputPath);

            // Access the global HeaderFooter object.
            HeaderFooter hf = diagram.HeaderFooter;

            // Extract header and footer text strings.
            string headerLeft   = hf.HeaderLeft   ?? string.Empty;
            string headerCenter = hf.HeaderCenter ?? string.Empty;
            string headerRight  = hf.HeaderRight  ?? string.Empty;

            string footerLeft   = hf.FooterLeft   ?? string.Empty;
            string footerCenter = hf.FooterCenter ?? string.Empty;
            string footerRight  = hf.FooterRight  ?? string.Empty;

            // Extract margins (in inches).
            double headerMargin = hf.HeaderMargin?.Value ?? 0.0;
            double footerMargin = hf.FooterMargin?.Value ?? 0.0;

            // Extract font information from the shared HeaderFooterFont.
            HeaderFooterFont font = hf.HeaderFooterFont;
            string fontFaceName = font.FaceName ?? string.Empty;
            int    fontHeight   = font.Height;          // Height is an integer (points).
            int    fontWeight   = font.Weight;          // 400 = normal, 700 = bold, etc.
            bool   fontItalic   = font.Italic == BOOL.True;
            bool   fontUnderline = font.Underline == BOOL.True;

            // Create an anonymous object to hold the configuration.
            var config = new
            {
                HeaderLeft   = headerLeft,
                HeaderCenter = headerCenter,
                HeaderRight  = headerRight,
                FooterLeft   = footerLeft,
                FooterCenter = footerCenter,
                FooterRight  = footerRight,
                HeaderMargin = headerMargin,
                FooterMargin = footerMargin,
                Font = new
                {
                    FaceName = fontFaceName,
                    Height   = fontHeight,
                    Weight   = fontWeight,
                    Italic   = fontItalic,
                    Underline = fontUnderline
                }
            };

            // Serialize the configuration to JSON with indentation for readability.
            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(config, jsonOptions);

            // Ensure the output directory exists.
            string outputDir = Path.GetDirectoryName(outputPath);
            if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }

            // Write JSON to the specified file.
            File.WriteAllText(outputPath, json);
            Console.WriteLine($"Header and footer configuration saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Log any exceptions that occur during processing.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}