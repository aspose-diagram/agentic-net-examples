using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving; // Required for shape operations per guidelines

class Program
{
    static void Main(string[] args)
    {
        // Determine the input Visio file path (first argument or default)
        string diagramPath = args.Length > 0 ? args[0] : "input.vsdx";

        // Guard: ensure the file exists before proceeding
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified file
            Diagram diagram = new Diagram(diagramPath);

            // Flag to indicate whether any shape violates the rule
            bool violationFound = false;

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through each shape on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Retrieve the DistanceFromGround value (in points) from the shape's 3D format
                    double distance = shape.ThreeDFormat.DistanceFromGround.Value;

                    // Validate that the distance does not exceed 100 points
                    if (distance > 100.0)
                    {
                        // Report the violation with shape ID and page name
                        Console.WriteLine($"Violation: Shape ID {shape.ID} on page \"{page.NameU}\" has DistanceFromGround = {distance} points (exceeds 100).");
                        violationFound = true;
                    }
                }
            }

            // Summarize the validation result
            if (!violationFound)
            {
                Console.WriteLine("All shapes have DistanceFromGround ≤ 100 points.");
            }
        }
        catch (Exception ex)
        {
            // Write any unexpected errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}