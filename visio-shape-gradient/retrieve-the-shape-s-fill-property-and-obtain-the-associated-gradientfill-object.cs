using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath>");
            return;
        }

        string diagramPath = args[0];

        // Guard against missing file.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Ensure the diagram contains at least one page.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("Error: The diagram does not contain any pages.");
                return;
            }

            // Retrieve the first page.
            Page page = diagram.Pages[0];

            // Ensure the page contains at least one shape.
            if (page.Shapes.Count == 0)
            {
                Console.Error.WriteLine("Error: The page does not contain any shapes.");
                return;
            }

            // Retrieve the first shape on the page.
            Shape shape = page.Shapes[0];

            // Access the Fill property of the shape.
            Fill fill = shape.Fill;

            // Obtain the associated GradientFill object.
            GradientFill gradientFill = fill.GradientFill;

            // Output basic gradient information.
            Console.WriteLine("=== Gradient Fill Information ===");
            Console.WriteLine($"Gradient Enabled: {gradientFill.GradientEnabled?.Value}");
            Console.WriteLine($"Gradient Direction: {gradientFill.GradientDir?.Value}");
            Console.WriteLine($"Gradient Angle: {gradientFill.GradientAngle?.Value}");

            // Output the number of gradient stops.
            int stopCount = gradientFill.GradientStops.Count;
            Console.WriteLine($"Number of Gradient Stops: {stopCount}");

            // Iterate through each gradient stop and display its position and color.
            for (int i = 0; i < stopCount; i++)
            {
                GradientStop stop = gradientFill.GradientStops[i];
                // Position is a DoubleValue; Color is a ColorValue.
                double? position = stop.Position?.Value;
                string color = stop.Color?.Value ?? "N/A";

                Console.WriteLine($"Stop {i + 1}: Position = {position}, Color = {color}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}