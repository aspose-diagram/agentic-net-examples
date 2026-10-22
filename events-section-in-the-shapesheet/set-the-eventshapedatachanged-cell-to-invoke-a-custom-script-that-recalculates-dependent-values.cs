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
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
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
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram has at least one page.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Work with the first page.
            Page page = diagram.Pages[0];

            // Ensure the page has at least one shape.
            if (page.Shapes.Count == 0)
            {
                Console.Error.WriteLine("The first page contains no shapes.");
                return;
            }

            // Retrieve the first shape that is not logically deleted.
            Shape targetShape = null;
            foreach (Shape shp in page.Shapes)
            {
                if (shp.Del == BOOL.False)
                {
                    targetShape = shp;
                    break;
                }
            }

            if (targetShape == null)
            {
                Console.Error.WriteLine("No non‑deleted shape found on the first page.");
                return;
            }

            // Set the EventShapeDataChanged equivalent.
            // The specific cell is not directly exposed in Aspose.Diagram, so we use EventDrop as the closest substitute.
            // The formula invokes a custom script named "RecalcScript" which should recalculate dependent values.
            targetShape.Event.EventDrop.Ufe.F = "RUNADDON(\"RecalcScript\")";

            // Save the modified diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}