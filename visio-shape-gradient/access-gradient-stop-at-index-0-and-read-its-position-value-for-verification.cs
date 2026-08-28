using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Access the first shape on the first page (adjust indices as needed)
            Shape shape = diagram.Pages[0].Shapes[0];

            // Get the gradient fill of the shape
            GradientFill gradientFill = shape.Fill.GradientFill;

            // Verify that gradient stops exist
            if (gradientFill != null && gradientFill.GradientStops.Count > 0)
            {
                // Access the gradient stop at index 0
                GradientStop firstStop = gradientFill.GradientStops[0];

                // Read its position value (DoubleValue.Value returns a double)
                double position = firstStop.Position.Value;

                // Output the position for verification
                Console.WriteLine($"Gradient stop at index 0 has position: {position}");
            }
            else
            {
                Console.WriteLine("No gradient stops found in the shape's gradient fill.");
            }

            // Save the diagram if any modifications were made
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
