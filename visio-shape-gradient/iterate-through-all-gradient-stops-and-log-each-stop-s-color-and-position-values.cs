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
            var diagram = new Diagram("input.vsdx");

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape has a gradient fill enabled
                    if (shape.Fill.GradientFill.GradientEnabled.Value == BOOL.True)
                    {
                        // Iterate over each gradient stop in the shape's gradient fill
                        foreach (GradientStop stop in shape.Fill.GradientFill.GradientStops)
                        {
                            double position = stop.Position.Value;   // Position (0.0 to 1.0)
                            string color = stop.Color.Value;        // Color as HEX string (e.g., "#FF0000")
                            Console.WriteLine($"Shape ID {shape.ID}: Stop Position = {position}, Color = {color}");
                        }
                    }
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
