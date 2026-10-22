using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output_scaled.vsdx";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram from the input file
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram has at least one page
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Work with the first page
            Page page = diagram.Pages[0];

            // Target shape ID (example uses shape ID 1 as per documentation)
            int targetShapeId = 1;

            // Retrieve the shape; GetShape returns null if not found
            Shape shape = page.Shapes.GetShape(targetShapeId);
            if (shape == null)
            {
                Console.Error.WriteLine($"Shape with ID {targetShapeId} not found on the first page.");
                return;
            }

            // Skip logically deleted shapes
            if (shape.Del == BOOL.True)
            {
                Console.Error.WriteLine($"Shape with ID {targetShapeId} is marked as deleted.");
                return;
            }

            // Read original dimensions
            double originalWidth = shape.XForm.Width.Value;
            double originalHeight = shape.XForm.Height.Value;

            // Scale factor
            double scaleFactor = 0.5;

            // Compute new dimensions
            double expectedWidth = originalWidth * scaleFactor;
            double expectedHeight = originalHeight * scaleFactor;

            // Apply scaling
            shape.XForm.Width.Value = expectedWidth;
            shape.XForm.Height.Value = expectedHeight;

            // Verify the dimensions after scaling
            double actualWidth = shape.XForm.Width.Value;
            double actualHeight = shape.XForm.Height.Value;

            const double tolerance = 1e-6;
            bool widthMatches = Math.Abs(actualWidth - expectedWidth) < tolerance;
            bool heightMatches = Math.Abs(actualHeight - expectedHeight) < tolerance;

            if (!widthMatches || !heightMatches)
            {
                throw new Exception($"Dimension verification failed. Expected Width={expectedWidth}, Height={expectedHeight}. " +
                                    $"Actual Width={actualWidth}, Height={actualHeight}.");
            }
            else
            {
                Console.WriteLine($"Shape ID {targetShapeId} scaled successfully. New Width={actualWidth}, Height={actualHeight}.");
            }

            // Save the modified diagram to the output file
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}