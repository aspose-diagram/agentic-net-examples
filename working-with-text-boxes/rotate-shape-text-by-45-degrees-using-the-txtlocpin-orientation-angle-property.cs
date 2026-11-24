using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output Visio file path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Ensure there is at least one page
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Use the first page
            Page page = diagram.Pages[0];

            // Ensure the page has at least one shape
            if (page.Shapes.Count == 0)
            {
                Console.Error.WriteLine("The first page contains no shapes.");
                return;
            }

            // Retrieve the first shape (you can modify selection logic as needed)
            Shape shape = page.Shapes.GetShape(1);

            // Guard: skip deleted shapes
            if (shape.Del == BOOL.True)
            {
                Console.Error.WriteLine("The target shape is marked as deleted.");
                return;
            }

            // Rotate the shape's text by 45 degrees.
            // TxtAngle expects radians, so convert from degrees.
            double angleDeg = 45.0;
            double angleRad = (Math.PI / 180.0) * angleDeg;
            shape.TextXForm.TxtAngle.Value = angleRad;

            // Optionally, you can adjust the text pivot point (TxtLocPin) if needed.
            // Here we keep the existing pivot; no changes are made.

            // Save the modified diagram to the specified output path in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}