using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input diagram path, shape ID, output diagram path
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <program> <input.vsdx> <shapeId> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Parse shape ID (long) from the second argument
        if (!long.TryParse(args[1], out long shapeId))
        {
            Console.Error.WriteLine($"Invalid shape ID: {args[1]}");
            return;
        }

        string outputPath = args[2];

        try
        {
            // Load the Visio diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Use the first page (index 0) – adjust if needed for multi‑page diagrams
            Page page = diagram.Pages[0];

            // Retrieve the shape by its ID; GetShape accepts a long identifier
            Shape shape = page.Shapes.GetShape(shapeId);
            if (shape == null)
            {
                Console.Error.WriteLine($"Shape with ID {shapeId} not found on page 0.");
                return;
            }

            // ----- Position text at the top of the shape -----
            // Set the local Y pin of the text block to 0 (top edge of the text block)
            shape.TextXForm.TxtLocPinY.Value = 0;

            // Align the text block's Y pin to the top edge of the shape (shape height)
            shape.TextXForm.TxtPinY.Value = shape.XForm.Height.Value;

            // ----- Configure text orientation angle -----
            // Example: set angle to 0 degrees (no rotation). Convert degrees to radians.
            double angleDegrees = 0; // modify as needed
            shape.TextXForm.TxtAngle.Value = (Math.PI / 180) * angleDegrees;

            // Save the modified diagram to the output file in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}