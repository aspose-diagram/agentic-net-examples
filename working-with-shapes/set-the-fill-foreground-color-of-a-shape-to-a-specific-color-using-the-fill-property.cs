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
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVisioFile>");
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
            // Load the Visio diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Retrieve the first page (or a specific page by name if required).
            // Here we use the first page in the collection.
            Page page = diagram.Pages[0];

            // Retrieve a shape by its ID. Adjust the ID as needed.
            // For demonstration we use shape ID 1.
            Shape shape = page.Shapes.GetShape(1);

            // Ensure the shape has a solid fill pattern; otherwise the fill may be invisible.
            shape.Fill.FillPattern.Value = 1; // 1 = solid fill.

            // Set the fill foreground color to a specific hexadecimal color.
            // Example color: bright green.
            shape.Fill.FillForegnd.Value = "#00FF00";

            // Save the modified diagram to the output path in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}