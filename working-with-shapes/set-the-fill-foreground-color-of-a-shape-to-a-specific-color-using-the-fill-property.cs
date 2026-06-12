using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Create a new diagram instance
            Diagram diagram = new Diagram();

            // Get the first page of the diagram
            Page page = diagram.Pages[0];

            // Add a rectangle shape at coordinates (2,2)
            long shapeId = page.AddShape(2.0, 2.0, "Rectangle");

            // Retrieve the shape object using its ID
            Shape shape = page.Shapes.GetShape(shapeId);

            // Set the fill foreground color to red (hex format)
            shape.Fill.FillForegnd.Value = "#FF0000";

            // Save the diagram to a VSDX file
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
