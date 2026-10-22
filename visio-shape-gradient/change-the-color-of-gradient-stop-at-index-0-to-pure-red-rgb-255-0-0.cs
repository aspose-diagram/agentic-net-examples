using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Get the first page (index 0). Adjust as needed for specific pages.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }
            Page page = diagram.Pages[0];

            // Get a shape by its ID. Here we use shape ID 1 as an example.
            // Ensure the shape exists on the page.
            Shape shape = null;
            foreach (Shape s in page.Shapes)
            {
                if (s.ID == 1)
                {
                    shape = s;
                    break;
                }
            }

            if (shape == null)
            {
                Console.Error.WriteLine("Shape with ID 1 was not found on the first page.");
                return;
            }

            // Ensure the shape has a gradient fill enabled.
            // If the shape already has a gradient, this will keep the existing settings.
            shape.Fill.FillPattern.Value = 25; // Gradient fill pattern.
            shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;

            // Access the gradient fill object.
            var gradientFill = shape.Fill.GradientFill;

            // Verify that at least one gradient stop exists.
            if (gradientFill.GradientStops.Count == 0)
            {
                Console.Error.WriteLine("The shape does not contain any gradient stops to modify.");
                return;
            }

            // Change the color of the gradient stop at index 0 to pure red (#FF0000).
            // Iterate to the first stop and modify its color.
            int currentIndex = 0;
            foreach (GradientStop stop in gradientFill.GradientStops)
            {
                if (currentIndex == 0)
                {
                    // Assign the hex color string directly to the Color cell's Value.
                    stop.Color.Value = "#FF0000";
                    break;
                }
                currentIndex++;
            }

            // Save the modified diagram to the specified output path in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}