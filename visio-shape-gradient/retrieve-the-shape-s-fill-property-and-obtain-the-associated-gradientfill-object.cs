using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath>");
            return;
        }

        string visioPath = args[0];

        // Guard against missing input file.
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"File not found: {visioPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(visioPath);

            // Ensure the diagram contains at least one page.
            if (diagram.Pages.Count == 0)
            {
                Console.WriteLine("The diagram contains no pages.");
                return;
            }

            // Get the first page.
            Page page = diagram.Pages[0];

            // Retrieve the first shape on the page.
            Shape shape = null;
            foreach (Shape s in page.Shapes)
            {
                shape = s;
                break;
            }

            if (shape == null)
            {
                Console.WriteLine("The page contains no shapes.");
                return;
            }

            // Access the Fill property of the shape.
            Fill fill = shape.Fill;

            // Retrieve the associated GradientFill object.
            GradientFill gradientFill = fill.GradientFill;

            // Output some gradient information.
            Console.WriteLine($"Shape ID: {shape.ID}");
            Console.WriteLine($"Gradient Enabled: {gradientFill.GradientEnabled.Value == BOOL.True}");
            Console.WriteLine($"Gradient Direction (GradientDir): {gradientFill.GradientDir.Value}");
            Console.WriteLine($"Gradient Angle: {gradientFill.GradientAngle.Value}");

            // Optionally, list gradient stops.
            Console.WriteLine("Gradient Stops:");
            foreach (GradientStop stop in gradientFill.GradientStops)
            {
                Console.WriteLine($"  Position: {stop.Position.Value}, Color: {stop.Color.Value}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}