using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Retrieve document title and version.
                string title = diagram.DocumentProps.Title ?? string.Empty;
                string version = diagram.Version ?? string.Empty;

                // Build the footer watermark text.
                string footerText = $"{title} v{version}";

                // Set the footer text (centered).
                diagram.HeaderFooter.FooterCenter = footerText;

                // Configure footer font appearance.
                var footerFont = diagram.HeaderFooter.HeaderFooterFont;
                footerFont.FaceName = "Arial";          // Font face.
                footerFont.Height = 12;                 // Font size (points).
                footerFont.Weight = 700;                // Bold weight.
                footerFont.Underline = BOOL.False;      // No underline.

                // Optionally adjust the footer margin (in inches from page edge).
                diagram.HeaderFooter.FooterMargin.Value = 0.5;

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}