using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;

class Program
{
    static void Main(string[] args)
    {
        // Determine the input Visio file path (first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

        // Guard: ensure the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block to ensure proper disposal
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page in the diagram
                foreach (Aspose.Diagram.Page page in diagram.Pages)
                {
                    // Create AutoSpaceOptions and set horizontal/vertical distances
                    AutoSpaceOptions options = new AutoSpaceOptions();
                    // Example distances (in inches); adjust as needed
                    options.DistanceInHorizontal = 0.5;
                    options.DistanceInVertical = 0.5;

                    // Auto-space all shapes on the current page using the options
                    page.AutoSpaceShapes(page.Shapes, options);

                    // After auto-spacing, log each non-deleted shape's new position
                    foreach (Aspose.Diagram.Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve shape ID and its PinX/PinY coordinates
                        long shapeId = shape.ID;
                        double pinX = shape.XForm.PinX.Value;
                        double pinY = shape.XForm.PinY.Value;

                        Console.WriteLine($"Page {page.ID}, Shape ID {shapeId}: PinX={pinX}, PinY={pinY}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}