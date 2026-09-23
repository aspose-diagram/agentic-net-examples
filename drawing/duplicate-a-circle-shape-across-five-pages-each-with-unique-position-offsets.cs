using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create a new empty diagram
        Diagram diagram = new Diagram();

        // Parameters for the base circle
        double basePinX = 2.0;      // X coordinate of the circle center
        double basePinY = 2.0;      // Y coordinate of the circle center
        double diameter = 1.0;      // Width and height (circle)
        double offsetStep = 0.5;    // Incremental offset for each page

        // Ensure the diagram has exactly five pages
        while (diagram.Pages.Count < 5)
        {
            diagram.Pages.Add(new Page());
        }

        // Create a circle on each page with a unique offset
        for (int i = 0; i < 5; i++)
        {
            Page page = diagram.Pages[i];

            double pinX = basePinX + i * offsetStep;
            double pinY = basePinY + i * offsetStep;

            // Draw an ellipse with equal width and height (a circle)
            long shapeId = page.DrawEllipse(pinX, pinY, diameter, diameter);

            // Retrieve the shape to apply additional formatting (optional)
            Shape circle = page.Shapes.GetShape(shapeId);
            circle.Fill.FillForegnd.Value = "#FF0000"; // Red fill color
        }

        // Save the diagram to a VSDX file
        diagram.Save("CircleDiagram.vsdx", SaveFileFormat.Vsdx);
    }
}
