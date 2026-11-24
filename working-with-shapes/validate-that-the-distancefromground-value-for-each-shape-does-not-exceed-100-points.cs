using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect the diagram file path as the first argument.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Error: No diagram file path provided.");
            return;
        }

        string diagramPath = args[0];

        // Guard: ensure the file exists before proceeding.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(diagramPath);

            bool anyViolation = false;

            // Iterate through each page in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through each shape on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the ThreeDFormat object exists.
                    if (shape.ThreeDFormat != null)
                    {
                        double distance = shape.ThreeDFormat.DistanceFromGround.Value;

                        // Validate that the distance does not exceed 100 points.
                        if (distance > 100.0)
                        {
                            anyViolation = true;
                            Console.WriteLine($"Violation: Shape ID {shape.ID} on page \"{page.Name}\" has DistanceFromGround = {distance} (exceeds 100).");
                        }
                    }
                }
            }

            if (!anyViolation)
            {
                Console.WriteLine("Validation passed: No shape exceeds a DistanceFromGround of 100 points.");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}