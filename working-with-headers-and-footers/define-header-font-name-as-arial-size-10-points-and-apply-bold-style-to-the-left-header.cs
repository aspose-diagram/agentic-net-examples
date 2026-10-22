using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments: input Visio file and output file.
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
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Access the global header/footer font settings.
            HeaderFooterFont headerFont = diagram.HeaderFooter.HeaderFooterFont;

            // Set the font face to Arial.
            headerFont.FaceName = "Arial";

            // Set the font size to 10 points.
            // Height uses the formula: Height = (PointSize * -1.333) rounded.
            // 10 * -1.333 = -13.33 → rounded to -13.
            headerFont.Height = -13;

            // Apply bold style by setting the weight to 700.
            headerFont.Weight = 700;

            // (Optional) Ensure underline is not applied.
            headerFont.Underline = BOOL.False;

            // Save the modified diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}