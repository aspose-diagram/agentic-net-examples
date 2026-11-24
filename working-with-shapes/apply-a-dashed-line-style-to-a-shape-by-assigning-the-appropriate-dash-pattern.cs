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

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Get the first page (or you could select by name).
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            Page page = diagram.Pages[0];

            // Ensure the page has at least one shape.
            if (page.Shapes.Count == 0)
            {
                Console.Error.WriteLine("The page contains no shapes.");
                return;
            }

            // Retrieve the first shape on the page.
            // Shapes are accessed via GetShape(shapeId). Here we use the first shape's ID.
            long firstShapeId = page.Shapes[0].ID;
            Shape shape = page.Shapes.GetShape(firstShapeId);

            // Apply a dashed line pattern to the shape.
            // Use the LinePatternValue enumeration for clarity.
            shape.Line.LinePattern.Value = LinePatternValue.Dash;

            // Optionally, you can also set line weight or color here.
            // shape.Line.LineWeight.Value = 0.03; // inches
            // shape.Line.LineColor.Value = "#FF0000"; // red

            // Ensure the output directory exists.
            string outputDir = Path.GetDirectoryName(outputPath);
            if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }

            // Save the modified diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}