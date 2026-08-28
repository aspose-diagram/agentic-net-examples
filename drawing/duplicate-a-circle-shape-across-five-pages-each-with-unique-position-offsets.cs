using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        // Create a new Visio diagram
        Aspose.Diagram.Diagram diagram = new Aspose.Diagram.Diagram();

        // Ensure the diagram has at least five pages
        while (diagram.Pages.Count < 5)
        {
            diagram.Pages.Add(new Aspose.Diagram.Page());
        }

        // Base position and size for the circle (ellipse with equal width and height)
        double basePinX = 2.0;      // starting X coordinate (in inches)
        double basePinY = 2.0;      // starting Y coordinate (in inches)
        double diameter = 1.0;      // circle diameter (in inches)

        // Add a circle to each of the five pages with a unique offset
        for (int i = 0; i < 5; i++)
        {
            // Calculate offset for the current page
            double offsetX = basePinX + i * 1.0;   // shift X by 1 inch per page
            double offsetY = basePinY + i * 0.5;   // shift Y by 0.5 inch per page

            // Get the page reference
            Aspose.Diagram.Page page = diagram.Pages[i];

            // Draw an ellipse where width == height, resulting in a circle
            page.DrawEllipse(offsetX, offsetY, diameter, diameter);
        }

        // Save the diagram to a file (VSDX format)
        diagram.Save("DuplicatedCircles.vsdx", Aspose.Diagram.SaveFileFormat.Vsdx);
    }
}
