using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Define the input Visio file path (adjust as needed)
        string inputPath = "input.vsdx";

        // Verify that the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram contains at least one page
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Access the first page of the diagram
            Page page = diagram.Pages[0];

            // Retrieve the first shape on the page (skip if no shapes)
            Shape shape = null;
            foreach (Shape s in page.Shapes)
            {
                shape = s;
                break;
            }

            if (shape == null)
            {
                Console.Error.WriteLine("No shapes found on the first page.");
                return;
            }

            // Store the original width and height of the shape
            double originalWidth = shape.XForm.Width.Value;
            double originalHeight = shape.XForm.Height.Value;

            // Compute the scaled dimensions (50% of original)
            double scaledWidth = originalWidth * 0.5;
            double scaledHeight = originalHeight * 0.5;

            // Apply the new scaled dimensions to the shape's XForm
            shape.XForm.Width.Value = scaledWidth;
            shape.XForm.Height.Value = scaledHeight;

            // Verify that the dimensions were updated correctly
            double actualWidth = shape.XForm.Width.Value;
            double actualHeight = shape.XForm.Height.Value;

            // Define a tolerance for floating‑point comparison
            const double tolerance = 1e-6;
            bool widthMatches = Math.Abs(actualWidth - scaledWidth) < tolerance;
            bool heightMatches = Math.Abs(actualHeight - scaledHeight) < tolerance;

            // Output verification results
            if (widthMatches && heightMatches)
            {
                Console.WriteLine("Shape dimensions successfully scaled by 0.5.");
                Console.WriteLine($"Original Width: {originalWidth}, New Width: {actualWidth}");
                Console.WriteLine($"Original Height: {originalHeight}, New Height: {actualHeight}");
            }
            else
            {
                Console.WriteLine("Dimension verification failed.");
                Console.WriteLine($"Expected Width: {scaledWidth}, Actual Width: {actualWidth}");
                Console.WriteLine($"Expected Height: {scaledHeight}, Actual Height: {actualHeight}");
            }

            // Optionally, save the modified diagram to a new file
            string outputPath = "output_scaled.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Modified diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose.Diagram errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}