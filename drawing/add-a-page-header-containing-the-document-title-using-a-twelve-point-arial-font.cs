using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVisioFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Retrieve the document title from built‑in properties.
            string title = diagram.DocumentProps.Title;

            // Set the header text (centered) to the document title.
            diagram.HeaderFooter.HeaderCenter = title;

            // Configure the header font: Arial, 12 pt.
            // 12 pt translates to a Height value of -16 (12 * -1.333 ≈ -16).
            var headerFont = diagram.HeaderFooter.HeaderFooterFont;
            headerFont.FaceName = "Arial";
            headerFont.Height = -16;          // 12 pt size.
            headerFont.Weight = 700;          // Bold weight (optional).

            // Save the modified diagram to the specified output path in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}