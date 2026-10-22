using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Ensure there is at least one page and one shape
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

            // Retrieve the first shape (as an example)
            Shape shape = page.Shapes.GetShape(page.Shapes[0].ID);

            // -----------------------------------------------------------------
            // Configure the event that fires when the shape is resized.
            // Aspose.Diagram does not expose a dedicated EventShapeResized cell.
            // The closest available event cell is EventXFMod, which is triggered
            // on shape modifications (including resize). We assign a formula
            // that maintains the aspect ratio by scaling Width based on Height.
            // -----------------------------------------------------------------
            // Formula explanation:
            //   GUARD(SETF(Width, Height * 1.5))
            //   - GUARD prevents recursive evaluation.
            //   - SETF updates the Width cell to keep a 1.5:1 aspect ratio.
            // Adjust the multiplier (1.5) as needed for your aspect ratio.
            // -----------------------------------------------------------------
            shape.Event.EventXFMod.Ufe.F = "GUARD(SETF(Width, Height*1.5))";

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}