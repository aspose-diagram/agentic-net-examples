using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Ensure there is at least one page
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Work with the first page
            Page page = diagram.Pages[0];

            // Find the first non-deleted shape to configure the event
            Shape targetShape = null;
            foreach (Shape shape in page.Shapes)
            {
                if (shape.Del == BOOL.False)
                {
                    targetShape = shape;
                    break;
                }
            }

            if (targetShape == null)
            {
                Console.Error.WriteLine("No non-deleted shape found on the first page.");
                return;
            }

            // Configure the event to maintain aspect ratio.
            // Aspose.Diagram does not expose EventShapeResized directly, so we use EventXFMod as the closest substitute.
            // The formula forces width and height to stay equal, effectively preserving the aspect ratio.
            targetShape.Event.EventXFMod.Ufe.F = "IF(Width<>Height, Width=Height, )";

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}