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
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
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

            // Ensure there is at least one page.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Get the first page.
            Page page = diagram.Pages[0];

            // Ensure the page has at least one shape.
            if (page.Shapes.Count == 0)
            {
                Console.Error.WriteLine("The first page contains no shapes.");
                return;
            }

            // Get the first shape on the page.
            Shape shape = page.Shapes[0];

            // Skip the shape if it is marked as deleted.
            if (shape.Del == BOOL.True)
            {
                Console.Error.WriteLine("The target shape is deleted; cannot modify.");
                return;
            }

            // ------------------------------------------------------------
            // Apply a 90‑degree orientation angle to the shape's text.
            // TextXForm.TxtAngle expects radians.
            // ------------------------------------------------------------
            double angleRadians = Math.PI / 2.0; // 90 degrees in radians
            shape.TextXForm.TxtAngle.Value = angleRadians;

            // ------------------------------------------------------------
            // Reposition the text to the left side of the shape.
            // TxtLocPinX is set to the text block width, and TxtPinX to 0.
            // ------------------------------------------------------------
            shape.TextXForm.TxtLocPinX.Value = shape.TextXForm.TxtWidth.Value;
            shape.TextXForm.TxtPinX.Value = 0;

            // ------------------------------------------------------------
            // Save the modified diagram to the specified output path.
            // ------------------------------------------------------------
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}