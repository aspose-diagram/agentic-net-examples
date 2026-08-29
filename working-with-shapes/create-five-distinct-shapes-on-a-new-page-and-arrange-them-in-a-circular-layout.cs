using System.IO;
using System;
using Aspose.Diagram;

class CircularShapesExample
{
    static void Main()
    {
        try
        {

            // Create a new diagram
            Diagram diagram = new Diagram();

            // Add a new page to the diagram
            Page page = new Page();
            diagram.Pages.Add(page);

            // Define center of the circle and radius (in inches)
            double centerX = 5.0;
            double centerY = 5.0;
            double radius = 3.0;

            // Masters to use for distinct shapes
            string[] masters = { "Rectangle", "Ellipse", "Triangle", "Hexagon", "Star" };

            // Place five shapes evenly around the circle
            for (int i = 0; i < 5; i++)
            {
                double angle = 2 * Math.PI * i / 5; // angle in radians
                double pinX = centerX + radius * Math.Cos(angle);
                double pinY = centerY + radius * Math.Sin(angle);

                // Add shape using the master name at calculated position
                page.AddShape(pinX, pinY, masters[i]);
            }

            // Optional: center drawing on the page (does not affect relative positions)
            page.CenterDrawing();

            // Save the diagram to a file
            diagram.Save("CircularShapes.vsdx", SaveFileFormat.Vsdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
