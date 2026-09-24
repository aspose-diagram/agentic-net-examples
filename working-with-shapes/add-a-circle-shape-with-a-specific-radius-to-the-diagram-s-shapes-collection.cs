using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Create a new empty diagram
        Diagram diagram = new Diagram();

        // Add a blank page (a new diagram has no pages by default)
        Page page = new Page();
        diagram.Pages.Add(page);

        // Define circle parameters (radius in inches)
        double radius = 1.5;          // radius of the circle
        double centerX = 5.0;         // X coordinate of the circle center
        double centerY = 5.0;         // Y coordinate of the circle center

        // Draw an ellipse with equal width and height to form a circle
        long shapeId = page.DrawEllipse(centerX, centerY, radius * 2, radius * 2);

        // Retrieve the created shape for further modifications if needed
        Shape circle = page.Shapes.GetShape(shapeId);

        // Example: set the fill color of the circle
        circle.Fill.FillForegnd.Value = "#ADD8E6"; // light blue

        // Save the diagram to a VSDX file
        diagram.Save("CircleDiagram.vsdx", SaveFileFormat.Vsdx);
    }
}
