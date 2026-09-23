using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Path to the Visio file to be loaded
        string inputPath = "input.vsdx";

        // Guard to ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Retrieve the first page (index 0) – Visio diagrams contain pages
            Page page = diagram.Pages[0];

            // Retrieve a shape (for example, the shape with ID 1) from the page
            Shape shape = page.Shapes.GetShape(1);

            // Enable gradient fill on the shape (optional, depends on the diagram)
            shape.Fill.FillPattern.Value = 25; // Gradient fill pattern
            shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;

            // Access the first gradient stop (index 0) and read its color value
            GradientStop firstStop = null;
            int currentIndex = 0;
            foreach (GradientStop stop in shape.Fill.GradientFill.GradientStops)
            {
                if (currentIndex == 0)
                {
                    firstStop = stop;
                    break;
                }
                currentIndex++;
            }

            if (firstStop != null)
            {
                // The color is stored as a hexadecimal string (e.g., "#FF0000")
                string colorHex = firstStop.Color.Value;
                Console.WriteLine($"First gradient stop color: {colorHex}");
            }
            else
            {
                Console.WriteLine("No gradient stops found on the shape.");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}