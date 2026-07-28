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

            // Add a rectangle shape on the first page (page index 0)
            // PinX = 2.0, PinY = 2.0 (in inches)
            long shapeId = diagram.AddShape(2.0, 2.0, "Rectangle", 0);

            // Retrieve the shape from the page's shape collection
            Page page = diagram.Pages[0];
            Shape shape = page.Shapes.GetShape(shapeId);

            // Set the fill pattern to solid (value 1)
            shape.Fill.FillPattern.Value = 1;

            // Apply a green foreground fill color (hex code)
            shape.Fill.FillForegnd.Value = "#00FF00";

            // Save the diagram to VSDX format
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
