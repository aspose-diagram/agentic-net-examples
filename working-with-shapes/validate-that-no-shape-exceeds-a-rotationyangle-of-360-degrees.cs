using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving; // Required for shape operations per rules

class Program
{
    static void Main(string[] args)
    {
        // Expect the first argument to be the Visio file path
        string filePath = args.Length > 0 ? args[0] : string.Empty;
        // Guard: ensure the file path is provided and the file exists
        if (string.IsNullOrWhiteSpace(filePath))
        {
            Console.Error.WriteLine("Error: No file path provided.");
            return;
        }
        if (!File.Exists(filePath))
        {
            Console.Error.WriteLine($"File not found: {filePath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(filePath);

            bool violationFound = false; // Track if any shape violates the rule

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Determine the zero‑based index of the current page within the diagram
                int pageIndex = diagram.Pages.IndexOf(page);

                // Iterate through each shape on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Access the Y‑axis rotation angle (in degrees) from the 3D format
                    double rotationY = shape.ThreeDFormat.RotationYAngle.Value;

                    // Check if the rotation exceeds 360 degrees
                    if (rotationY > 360.0)
                    {
                        // Report the offending shape with its ID, page index, and rotation value
                        Console.Error.WriteLine($"Violation: Shape ID {shape.ID} on page index {pageIndex} has RotationYAngle = {rotationY}° (exceeds 360°).");
                        violationFound = true;
                    }
                }
            }

            // If no violations were detected, output a success message
            if (!violationFound)
            {
                Console.WriteLine("Validation passed: No shape exceeds a RotationYAngle of 360 degrees.");
            }
        }
        catch (Exception ex)
        {
            // Write any unexpected errors to the error stream
            Console.Error.WriteLine($"An error occurred while processing the diagram: {ex.Message}");
        }
    }
}