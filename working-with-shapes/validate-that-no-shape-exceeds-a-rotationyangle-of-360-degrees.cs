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
            Console.Error.WriteLine("Usage: Program <input-diagram-file>");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            bool hasInvalidRotation = false;

            // Iterate through all pages.
            for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
            {
                Page page = diagram.Pages[pageIndex];

                // Iterate through all shapes on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Retrieve the Y‑axis rotation angle.
                    double rotationY = shape.ThreeDFormat.RotationYAngle.Value;

                    // Validate the rotation does not exceed 360 degrees.
                    if (rotationY > 360.0)
                    {
                        hasInvalidRotation = true;
                        Console.WriteLine($"Warning: Shape ID {shape.ID} on page {pageIndex} has RotationYAngle = {rotationY}° (exceeds 360°).");
                    }
                }
            }

            if (!hasInvalidRotation)
            {
                Console.WriteLine("Validation passed: No shape exceeds a RotationYAngle of 360 degrees.");
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}