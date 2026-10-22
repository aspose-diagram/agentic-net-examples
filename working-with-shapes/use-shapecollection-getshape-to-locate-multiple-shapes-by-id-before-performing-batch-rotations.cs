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
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Define the IDs of the shapes we want to rotate
            long[] shapeIds = new long[] { 1, 2, 3 }; // Example IDs; replace with actual IDs

            // Desired rotation angle in degrees
            double rotationAngle = 45.0;

            // Iterate over each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Process each target shape ID
                foreach (long id in shapeIds)
                {
                    // Retrieve the shape by ID using GetShape
                    Shape shape = page.Shapes.GetShape(id);
                    if (shape == null)
                    {
                        Console.Error.WriteLine($"Warning: Shape with ID {id} not found on page {page.Name}.");
                        continue;
                    }

                    // Skip logically deleted shapes
                    if (shape.Del == BOOL.True)
                    {
                        continue;
                    }

                    // Apply rotation by setting the Angle cell value (degrees)
                    shape.XForm.Angle.Value = rotationAngle;
                }
            }

            // Save the modified diagram to the output path
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}