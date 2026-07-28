using System.IO;
using System;
using Aspose.Diagram;

class Program
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

            // Parameters for the circular layout
            double centerX = 5.0;      // Center X coordinate (in inches)
            double centerY = 5.0;      // Center Y coordinate (in inches)
            double radius = 3.0;       // Radius of the circle (in inches)
            string masterName = "Rectangle"; // Master shape to use for each shape

            // Add five shapes positioned evenly around the circle
            for (int i = 0; i < 5; i++)
            {
                double angle = 2 * Math.PI * i / 5; // Angle in radians
                double pinX = centerX + radius * Math.Cos(angle);
                double pinY = centerY + radius * Math.Sin(angle);

                // Use the AddShape method that takes PinX, PinY, and master name
                page.AddShape(pinX, pinY, masterName);
            }

            // Optional: apply Visio's built‑in circular layout (requires LayoutOptions)
            // LayoutOptions layoutOpts = new LayoutOptions();
            // layoutOpts.PlaceStyle = PlaceStyleValue.Circular;
            // page.Layout(layoutOpts);

            // Save the diagram to a file
            diagram.Save("CircularShapes.vsdx", SaveFileFormat.Vsdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
