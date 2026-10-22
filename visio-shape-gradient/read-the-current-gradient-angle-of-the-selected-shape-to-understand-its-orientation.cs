using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the Visio diagram file as the first argument.");
            return;
        }

        string diagramPath = args[0];

        // Guard: ensure the file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Get the active page
            Page activePage = diagram.ActivePage;
            if (activePage == null)
            {
                Console.Error.WriteLine("The diagram does not contain an active page.");
                return;
            }

            // Ensure there is at least one shape on the page
            if (activePage.Shapes.Count == 0)
            {
                Console.Error.WriteLine("No shapes found on the active page.");
                return;
            }

            // Retrieve the first shape (as the selected shape)
            Shape shape = activePage.Shapes[0];

            // Check if the shape has a gradient fill enabled
            if (shape.Fill != null &&
                shape.Fill.GradientFill != null &&
                shape.Fill.GradientFill.GradientEnabled != null &&
                shape.Fill.GradientFill.GradientEnabled.Value == BOOL.True)
            {
                // Read the current gradient angle (in degrees)
                double gradientAngle = shape.Fill.GradientFill.GradientAngle.Value;
                Console.WriteLine($"Gradient angle of the selected shape: {gradientAngle} degrees");
            }
            else
            {
                Console.WriteLine("The selected shape does not have a gradient fill enabled.");
            }
        }
        catch (Exception ex)
        {
            // Output any errors that occur during processing
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}