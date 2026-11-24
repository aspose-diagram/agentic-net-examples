using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input diagram path, output diagram path, shape ID (numeric).
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputPath> <outputPath> <shapeId>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string shapeIdArg = args[2];

        // Guard: verify input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Parse shape ID.
        if (!long.TryParse(shapeIdArg, out long shapeId))
        {
            Console.Error.WriteLine($"Invalid shape ID: {shapeIdArg}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Retrieve the first page (index 0). Adjust if needed.
            Page page = diagram.Pages[0];

            // Retrieve the shape by its ID. GetShape expects an int identifier.
            Shape shape = page.Shapes.GetShape((int)shapeId);

            // Ensure the shape is not logically deleted.
            if (shape.Del == BOOL.True)
            {
                Console.Error.WriteLine($"Shape with ID {shapeId} is marked as deleted.");
                return;
            }

            // Align text to the right side of the shape:
            // - Set the text block's pin (TxtPinX) to the shape's right edge.
            // - Set the text block's location pin (TxtLocPinX) to the left edge (0).
            // This positions the text block so its right side aligns with the shape's right side.
            shape.TextXForm.TxtPinX.Value = shape.XForm.Width.Value; // Right edge of shape.
            shape.TextXForm.TxtLocPinX.Value = 0;                    // Left edge of text block.

            // Optional: ensure no rotation is applied to the text.
            shape.TextXForm.TxtAngle.Value = 0;

            // Save the modified diagram to the specified output path in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}