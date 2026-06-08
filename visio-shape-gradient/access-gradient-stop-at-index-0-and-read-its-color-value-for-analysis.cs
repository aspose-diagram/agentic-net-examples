using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your file path)
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Access the first page
            Page page = diagram.Pages[0];

            // Access a shape on the page (shape IDs start at 1)
            // Adjust the ID as needed for your specific diagram
            Shape shape = page.Shapes.GetShape(1);

            // Ensure the shape has a gradient fill
            GradientFill gradientFill = shape.Fill.GradientFill;

            // Access the gradient stops collection
            GradientStopCollection stops = gradientFill.GradientStops;

            // Verify that at least one gradient stop exists
            if (stops.Count == 0)
            {
                Console.WriteLine("No gradient stops found on the shape.");
                return;
            }

            // Retrieve the first gradient stop (index 0)
            GradientStop firstStop = null;
            int currentIndex = 0;
            foreach (GradientStop stop in stops)
            {
                if (currentIndex == 0)
                {
                    firstStop = stop;
                    break;
                }
                currentIndex++;
            }

            if (firstStop == null)
            {
                Console.WriteLine("Unable to retrieve the first gradient stop.");
                return;
            }

            // Read the color value of the first gradient stop
            string colorHex = firstStop.Color.Value; // e.g., "#FF0000"

            // Output the color value for analysis
            Console.WriteLine($"First gradient stop color: {colorHex}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
