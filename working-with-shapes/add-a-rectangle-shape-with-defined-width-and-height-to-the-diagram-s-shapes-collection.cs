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

            // Add a new blank page to the diagram
            diagram.Pages.Add(new Page());

            // Get the first page
            Page page = diagram.Pages[0];

            // Define rectangle parameters (in inches)
            double pinX = 2.0;   // X coordinate of the shape's pin (center)
            double pinY = 2.0;   // Y coordinate of the shape's pin (center)
            double width = 3.0;  // Width of the rectangle
            double height = 2.0; // Height of the rectangle

            // Add a rectangle shape using the built‑in "Rectangle" master
            long rectId = page.AddShape(pinX, pinY, width, height, "Rectangle");

            // Retrieve the shape object for further modifications if needed
            Shape rectangle = page.Shapes.GetShape(rectId);

            // Optional: set the fill color of the rectangle
            rectangle.Fill.FillForegnd.Value = "#FF0000";

            // Optional: save the diagram to a VSDX file
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
