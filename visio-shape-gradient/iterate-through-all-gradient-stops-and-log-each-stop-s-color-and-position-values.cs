using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the diagram (replace with your actual file path)
            string diagramPath = "input.vsdx";
            Diagram diagram = new Diagram(diagramPath);

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through each shape on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Check if the shape has gradient fill enabled
                    if (shape.Fill.GradientFill.GradientEnabled.Value == BOOL.True)
                    {
                        // Iterate through all gradient stops of the shape
                        foreach (GradientStop stop in shape.Fill.GradientFill.GradientStops)
                        {
                            double position = stop.Position.Value;   // Position (0 to 1)
                            string color = stop.Color.Value;        // Color as hex string (e.g., "#FF0000")
                            Console.WriteLine($"Shape ID {shape.ID}: Position = {position}, Color = {color}");
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
