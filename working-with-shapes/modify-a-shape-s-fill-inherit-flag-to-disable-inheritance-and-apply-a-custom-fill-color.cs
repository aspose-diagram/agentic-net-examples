using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect four arguments: input diagram, output diagram, shape ID, fill color (hex).
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: <inputPath> <outputPath> <shapeId> <hexColor>");
            return;
        }

        // Assign arguments to variables.
        string inputPath = args[0];
        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath)) { Console.Error.WriteLine($"File not found: {inputPath}"); return; }

        string outputPath = args[1];
        string shapeIdStr = args[2];
        string hexColor = args[3];

        // Parse the shape ID; guard against invalid format.
        if (!long.TryParse(shapeIdStr, out long shapeId))
        {
            Console.Error.WriteLine($"Invalid shape ID: {shapeIdStr}");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Retrieve the first page (index 0) – adjust if needed for multi‑page docs.
            Page page = diagram.Pages[0];

            // Get the shape by its ID; guard against a missing shape.
            Shape shape = page.Shapes.GetShape(shapeId);
            if (shape == null)
            {
                Console.Error.WriteLine($"Shape with ID {shapeId} not found on page 0.");
                return;
            }

            // Ensure a solid fill pattern (1 = solid).
            shape.Fill.FillPattern.Value = 1; // solid fill

            // Apply the custom foreground fill color (hex string, e.g., "#FF0000").
            shape.Fill.FillForegnd.Value = hexColor; // custom color

            // Save the modified diagram; using VSDX as a common output format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Shape {shapeId} updated and diagram saved to {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}