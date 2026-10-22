using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages (required by task, even though header font is global)
            foreach (Page page in diagram.Pages)
            {
                // No per-page header font; modify the global header/footer font
                // Set the localized font name (example: Arial Unicode MS)
                diagram.HeaderFooter.HeaderFooterFont.FaceName = "Arial Unicode MS";

                // Optionally adjust other typography attributes
                // Height uses a negative value to represent point size (e.g., -16 for 12pt)
                diagram.HeaderFooter.HeaderFooterFont.Height = -16;
                // Set regular weight (400) – use 700 for bold if desired
                diagram.HeaderFooter.HeaderFooterFont.Weight = 400;
                // Ensure underline and italic are disabled
                diagram.HeaderFooter.HeaderFooterFont.Underline = BOOL.False;
                diagram.HeaderFooter.HeaderFooterFont.Italic = BOOL.False;
            }

            // Save the modified diagram to the output path in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}