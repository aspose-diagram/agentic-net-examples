using System.IO;
using Aspose.Diagram;
using System;

class Program
{
    static void Main()
    {
        try
        {

            // Create a new diagram instance
            Diagram diagram = new Diagram();

            // Add a new page to the diagram
            Page page = new Page();
            diagram.Pages.Add(page);

            // Parameters for the circular layout
            double centerX = 5.0;      // X‑coordinate of circle centre (in inches)
            double centerY = 5.0;      // Y‑coordinate of circle centre (in inches)
            double radius = 3.0;       // Radius of the circle (in inches)
            int shapeCount = 5;        // Number of shapes to place

            // Master shape names – each will produce a distinct visual shape
            string[] masterNames = { "Rectangle", "Ellipse", "Triangle", "Hexagon", "Star" };

            // Add each shape at its calculated position on the circle
            for (int i = 0; i < shapeCount; i++)
            {
                double angle = 2 * Math.PI * i / shapeCount;               // Angle for current shape
                double pinX = centerX + radius * Math.Cos(angle);          // X coordinate on circle
                double pinY = centerY + radius * Math.Sin(angle);          // Y coordinate on circle

                // Add the shape with a default size of 1 inch by 1 inch
                page.AddShape(pinX, pinY, 1.0, 1.0, masterNames[i]);
            }

            // Center the drawing on the page (optional, keeps the group centered)
            page.CenterDrawing();

            // Save the diagram to a VSDX file
            diagram.Save("CircularShapes.vsdx", SaveFileFormat.Vsdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
