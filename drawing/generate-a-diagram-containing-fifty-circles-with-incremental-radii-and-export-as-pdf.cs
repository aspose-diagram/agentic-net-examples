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

        // Add a new page to the diagram
        Page page = new Page();
        diagram.Pages.Add(page);

        // Center coordinates for all circles
        double centerX = 300.0;
        double centerY = 300.0;

        // Incremental radius step
        double radiusStep = 5.0;

        // Draw 50 circles with increasing radii
        for (int i = 1; i <= 50; i++)
        {
            double radius = i * radiusStep;
            double diameter = radius * 2.0;

            // DrawEllipse draws an ellipse; using equal width and height creates a circle
            page.DrawEllipse(centerX, centerY, diameter, diameter);
        }

        // Export the diagram to PDF using the provided Save method
        diagram.Save("Circles.pdf", SaveFileFormat.Pdf);
    }
}
