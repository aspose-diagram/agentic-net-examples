using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create a new diagram instance
        Diagram diagram = new Diagram();

        // Add a new page to the diagram
        Page newPage = new Page();
        diagram.Pages.Add(newPage);

        // Coordinates (in inches) where the oval will be placed
        double pinX = 5.0;   // X‑coordinate of the shape's pin (center)
        double pinY = 7.0;   // Y‑coordinate of the shape's pin (center)

        // Size of the oval (in inches)
        double width = 3.0;  // Width of the ellipse
        double height = 2.0; // Height of the ellipse

        // Draw the oval (ellipse) on the newly added page
        newPage.DrawEllipse(pinX, pinY, width, height);

        // Save the diagram to a VDX file
        diagram.Save("OvalShape.vdx", SaveFileFormat.Vdx);
    }
}
