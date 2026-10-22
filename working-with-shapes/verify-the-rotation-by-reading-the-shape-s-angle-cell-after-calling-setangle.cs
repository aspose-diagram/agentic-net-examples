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
            Console.Error.WriteLine("Please provide the path to the Visio file as the first argument.");
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

            // Ensure there is at least one page
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Work with the first page
            Page page = diagram.Pages[0];

            // Find the first non-deleted shape on the page
            Shape targetShape = null;
            foreach (Shape shape in page.Shapes)
            {
                // Skip logically deleted shapes
                if (shape.Del == BOOL.True)
                    continue;

                targetShape = shape;
                break;
            }

            if (targetShape == null)
            {
                Console.Error.WriteLine("No non-deleted shapes were found on the first page.");
                return;
            }

            // Desired rotation angle in degrees
            double desiredAngle = 45.0;

            // Rotate the shape using SetAngle
            targetShape.SetAngle(desiredAngle);

            // Read back the Angle cell value to verify
            double actualAngle = targetShape.XForm.Angle.Value;

            // Output verification result
            Console.WriteLine($"Shape ID {targetShape.ID} rotated to {actualAngle} degrees (expected {desiredAngle}).");

            // Simple validation with tolerance
            const double tolerance = 0.0001;
            if (Math.Abs(actualAngle - desiredAngle) > tolerance)
            {
                throw new Exception($"Angle verification failed. Expected {desiredAngle}, but got {actualAngle}.");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}