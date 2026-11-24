using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        // Output Visio file path
        string outputPath = "output.vsdx";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found at '{inputPath}'.");
            return;
        }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Define the shape IDs that need to be rotated
            // These IDs should correspond to existing shapes in the diagram
            int[] shapeIdsToRotate = new int[] { 1, 2, 3 };

            // Define the rotation angle (in degrees) to apply to each shape
            double rotationAngle = 45.0;

            // Iterate over each page in the diagram (commonly there is only one page)
            foreach (Page page in diagram.Pages)
            {
                // Process each target shape ID
                foreach (int shapeId in shapeIdsToRotate)
                {
                    // Retrieve the shape by its ID using GetShape
                    Shape shape = page.Shapes.GetShape(shapeId);

                    // Validate that the shape exists and is not logically deleted
                    if (shape == null)
                    {
                        Console.Error.WriteLine($"Warning: Shape with ID {shapeId} not found on page {page.Name}.");
                        continue;
                    }

                    if (shape.Del == BOOL.True)
                    {
                        // Skip shapes that are marked as deleted
                        continue;
                    }

                    // Apply the rotation angle to the shape
                    // The Angle cell expects the value in degrees
                    shape.XForm.Angle.Value = rotationAngle;
                }
            }

            // Save the modified diagram to the output file in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram related errors
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}