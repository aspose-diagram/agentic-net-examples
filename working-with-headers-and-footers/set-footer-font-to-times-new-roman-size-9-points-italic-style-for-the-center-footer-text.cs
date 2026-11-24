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

        // Guard against missing input file.
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
                // Access the HeaderFooter object.
                HeaderFooter headerFooter = diagram.HeaderFooter;

                // Configure the footer font: Times New Roman, 9 pt, italic.
                // Height uses a negative mapping: (pt * -1.333) rounded.
                // 9 pt → -12 (approx).
                headerFooter.HeaderFooterFont.FaceName = "Times New Roman";
                headerFooter.HeaderFooterFont.Height = -12;
                headerFooter.HeaderFooterFont.Italic = BOOL.True;

                // (Optional) Ensure underline is disabled.
                headerFooter.HeaderFooterFont.Underline = BOOL.False;

                // Save the modified diagram in VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}