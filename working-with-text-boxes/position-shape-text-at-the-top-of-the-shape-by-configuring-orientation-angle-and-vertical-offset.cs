using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output Visio file path.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: verify input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // For demonstration, target the first shape on the first page.
            // Adjust the selection logic as needed (e.g., by shape name or ID).
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            Page page = diagram.Pages[0];

            if (page.Shapes.Count == 0)
            {
                Console.Error.WriteLine("The first page contains no shapes.");
                return;
            }

            // Retrieve the first shape (ID is a long).
            Shape shape = page.Shapes[0];

            // Ensure the shape is not deleted.
            if (shape.Del == BOOL.True)
            {
                Console.Error.WriteLine("The selected shape is marked as deleted.");
                return;
            }

            // ------------------------------
            // Position text at the top of the shape.
            // ------------------------------

            // Set vertical offset: locate the text block's local pin at Y = 0 (top)
            shape.TextXForm.TxtLocPinY.Value = 0;

            // Position the text block's pin so that its bottom aligns with the shape's top.
            // This uses the shape's height to place the text block correctly.
            shape.TextXForm.TxtPinY.Value = shape.XForm.Height.Value;

            // Set text orientation angle to 0 radians (no rotation).
            shape.TextXForm.TxtAngle.Value = 0;

            // Optionally, you can also reset any existing text rotation on the shape itself.
            shape.XForm.Angle.Value = 0;

            // Save the modified diagram to the specified output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}