using System.IO;
using System;
using Aspose.Diagram;

class GradientStopReader
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Access the first page (index 0) and the first shape on that page (index 1)
            // (index 0 on a page is the page itself, so shapes start at 1)
            Shape shape = diagram.Pages[0].Shapes[1];

            // Ensure the shape has a fill and that fill is a gradient
            if (shape.Fill != null && shape.Fill.GradientFill != null)
            {
                // Get the gradient stop collection
                GradientStopCollection stops = shape.Fill.GradientFill.GradientStops;

                // Verify that at least one gradient stop exists
                if (stops.Count > 0)
                {
                    // Access the gradient stop at index 0
                    GradientStop firstStop = stops[0];

                    // Read its position value (DoubleValue contains a .Value property)
                    double position = firstStop.Position.Value;

                    // Output the position for verification
                    Console.WriteLine($"First gradient stop position: {position}");
                }
                else
                {
                    Console.WriteLine("No gradient stops found in the collection.");
                }
            }
            else
            {
                Console.WriteLine("The selected shape does not have a gradient fill.");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
