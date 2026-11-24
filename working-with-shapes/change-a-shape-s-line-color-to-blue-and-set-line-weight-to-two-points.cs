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
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
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
            // Load the Visio diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Get the first page in the diagram.
            // (Assumes the diagram contains at least one page.)
            Page page = diagram.Pages[0];

            // Get the first shape on the page.
            // (Assumes the page contains at least one shape.)
            Shape shape = page.Shapes[0];

            // Set the shape's line color to blue (hex format).
            shape.Line.LineColor.Value = "#0000FF";

            // Set the line weight to two points.
            // One point = 1/72 inch, so two points = 2/72 inches.
            shape.Line.LineWeight.Value = 2.0 / 72.0;

            // Save the modified diagram to the output path in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}