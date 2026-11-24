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
            Console.Error.WriteLine("Error: No input file path provided.");
            return;
        }

        string inputPath = args[0];

        // Guard against missing file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – \"{inputPath}\"");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            bool anyInvalid = false;

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

                    // Validate the angle does not exceed 360 degrees.
                    if (rotationY > 360.0)
                    {
                        anyInvalid = true;
                        Console.WriteLine($"Page {pageIndex + 1}, Shape ID {shape.ID} has RotationYAngle = {rotationY}° (exceeds 360°).");
                    }
                }
            }

            if (!anyInvalid)
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