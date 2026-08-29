using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Get the first (default) page
            Page page = diagram.Pages[0];

            // Add a rectangle shape to the page at position (2,2)
            long shapeId = page.AddShape(2.0, 2.0, "Rectangle");

            // Retrieve the shape instance using its ID
            Shape shape = page.Shapes.GetShape(shapeId);

            // Set the line color to blue (hex format)
            shape.Line.LineColor.Value = "#0000FF";

            // Set the line weight to two points (2/72 inches)
            shape.Line.LineWeight.Value = 2.0 / 72.0;

            // Save the diagram as VSDX
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
