using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath>");
            return;
        }

        string diagramPath = args[0];

        // Guard against missing file
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Ensure there is at least one page
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Get the first page
            Page page = diagram.Pages[0];

            // Ensure there is at least one shape on the page
            if (page.Shapes.Count == 0)
            {
                Console.Error.WriteLine("The first page contains no shapes.");
                return;
            }

            // Retrieve the first shape (shape IDs start at 1)
            Shape shape = page.Shapes.GetShape(1);

            // Enable gradient fill if not already enabled
            shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;

            // Access the gradient stops collection
            var gradientStops = shape.Fill.GradientFill.GradientStops;

            // Retrieve current stop count
            int stopCount = gradientStops.Count;
            Console.WriteLine($"Current gradient stop count: {stopCount}");

            // Ensure a minimum of three gradient stops
            if (stopCount < 3)
            {
                Console.WriteLine("Adding missing gradient stops to reach a minimum of three.");

                // Define default positions and colors for the stops
                // Positions are normalized (0 to 1) using MeasureConst.NUM
                // Colors are defined with hex strings and MeasureConst.Undefined
                DoubleValue[] positions = new DoubleValue[]
                {
                    new DoubleValue(0.0, MeasureConst.NUM),
                    new DoubleValue(0.5, MeasureConst.NUM),
                    new DoubleValue(1.0, MeasureConst.NUM)
                };

                ColorValue[] colors = new ColorValue[]
                {
                    new ColorValue("#FF0000", MeasureConst.Undefined), // Red
                    new ColorValue("#00FF00", MeasureConst.Undefined), // Green
                    new ColorValue("#0000FF", MeasureConst.Undefined)  // Blue
                };

                // Clear existing stops to avoid duplicates
                gradientStops.Clear();

                // Add three stops
                for (int i = 0; i < 3; i++)
                {
                    gradientStops.Add(positions[i], colors[i]);
                }

                Console.WriteLine("Gradient stops have been set to three default values.");
            }
            else
            {
                Console.WriteLine("The gradient already has three or more stops.");
            }

            // Optionally save the modified diagram to a new file
            string outputPath = Path.Combine(Path.GetDirectoryName(diagramPath) ?? "", "Modified_" + Path.GetFileName(diagramPath));
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Modified diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}