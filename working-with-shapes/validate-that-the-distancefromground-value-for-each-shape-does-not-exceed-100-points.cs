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

            bool anyViolation = false;

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the ThreeDFormat object is available
                    if (shape.ThreeDFormat != null)
                    {
                        // Retrieve the DistanceFromGround value (in points)
                        double distance = shape.ThreeDFormat.DistanceFromGround.Value;

                        // Validate that the distance does not exceed 100 points
                        if (distance > 100.0)
                        {
                            anyViolation = true;
                            Console.WriteLine($"Violation: Shape ID {shape.ID} on page '{page.Name}' has DistanceFromGround = {distance} (exceeds 100).");
                        }
                    }
                }
            }

            if (!anyViolation)
            {
                Console.WriteLine("All shapes have DistanceFromGround <= 100 points.");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}