using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Define input and output file paths
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output.vsdx";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Optional: set the center footer text
                diagram.HeaderFooter.FooterCenter = "Sample Footer Center";

                // Access the footer font settings
                HeaderFooterFont footerFont = diagram.HeaderFooter.HeaderFooterFont;

                // Set the font face to Times New Roman
                footerFont.FaceName = "Times New Roman";

                // Set the font size to 9 points.
                // Height uses a negative mapping: Height = DesiredPointSize * -1.333 (rounded)
                // 9 * -1.333 ≈ -12
                footerFont.Height = -12;

                // Apply italic style
                footerFont.Italic = BOOL.True;

                // Ensure underline is disabled
                footerFont.Underline = BOOL.False;

                // Set regular weight (400) for non‑bold text
                footerFont.Weight = 400;

                // Save the modified diagram in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}