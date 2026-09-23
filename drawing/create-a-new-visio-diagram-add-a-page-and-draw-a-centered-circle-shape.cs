using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create a new empty Visio diagram
        Diagram diagram = new Diagram();

        // Add a new page to the diagram
        Page page = new Page();
        diagram.Pages.Add(page);

        // Retrieve the page dimensions (in inches)
        double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
        double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

        // Define circle parameters
        double radius = 1.0;               // radius in inches
        double diameter = radius * 2;      // width and height for the circle
        double centerX = pageWidth / 2;    // X coordinate of the circle center
        double centerY = pageHeight / 2;   // Y coordinate of the circle center

        // Draw a centered circle (ellipse with equal width and height)
        long shapeId = page.DrawEllipse(centerX, centerY, diameter, diameter);

        // Save the diagram to a VSDX file
        diagram.Save("CenteredCircle.vsdx", SaveFileFormat.Vsdx);
    }
}
