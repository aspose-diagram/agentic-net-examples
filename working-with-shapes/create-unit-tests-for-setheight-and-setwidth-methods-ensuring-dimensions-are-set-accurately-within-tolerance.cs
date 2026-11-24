using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect at least one argument: path to the input Visio diagram file.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath>");
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
            // Load the diagram.
            Diagram diagram = new Diagram(inputPath);

            // Assume the shape with ID 1 exists (commonly a rectangle in sample files).
            // Retrieve the shape from the first page.
            Page page = diagram.Pages[0];
            long shapeId = 1;
            Shape shape = page.Shapes.GetShape(shapeId);

            // Verify the shape was retrieved.
            if (shape == null)
            {
                throw new Exception($"Shape with ID {shapeId} not found.");
            }

            // Store original dimensions for reference (optional).
            double originalHeight = shape.XForm.Height.Value;
            double originalWidth = shape.XForm.Width.Value;

            // Define new dimensions to set.
            double newHeight = originalHeight + 1.0; // increase by 1 inch
            double newWidth = originalWidth + 2.0;   // increase by 2 inches

            // Set height and width using the provided methods.
            shape.SetHeight(newHeight);
            shape.SetWidth(newWidth);

            // Tolerance for floating‑point comparison.
            const double tolerance = 0.001;

            // Validate height.
            double actualHeight = shape.XForm.Height.Value;
            if (Math.Abs(actualHeight - newHeight) > tolerance)
            {
                throw new Exception($"Height verification failed. Expected: {newHeight}, Actual: {actualHeight}");
            }

            // Validate width.
            double actualWidth = shape.XForm.Width.Value;
            if (Math.Abs(actualWidth - newWidth) > tolerance)
            {
                throw new Exception($"Width verification failed. Expected: {newWidth}, Actual: {actualWidth}");
            }

            // If we reach this point, the test passed.
            Console.WriteLine("SetHeight and SetWidth tests passed successfully.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}