using System.IO;
using Aspose.Diagram;
using System;

class Program
{
    static void Main()
    {
        // Create a new diagram (lifecycle rule)
        Diagram diagram = new Diagram();

        // Add a new page to the diagram
        Page page = new Page();
        diagram.Pages.Add(page);

        // Conversion factor from centimeters to inches (Aspose.Diagram uses inches)
        const double cmToInch = 0.393700787;

        // Desired circle parameters in centimeters
        double centerXcm = 5.0;   // X‑coordinate of the circle centre
        double centerYcm = 10.0;  // Y‑coordinate of the circle centre
        double radiusCm = 2.0;    // Radius of the circle

        // Convert coordinates and size to inches
        double centerXinch = centerXcm * cmToInch;
        double centerYinch = centerYcm * cmToInch;
        double diameterInch = radiusCm * 2 * cmToInch;

        // Draw a circle (ellipse with equal width and height) at the specified position
        // DrawEllipse returns the shape ID; it is not needed for further processing here
        page.DrawEllipse(centerXinch, centerYinch, diameterInch, diameterInch);

        // Save the diagram (lifecycle rule)
        diagram.Save("CircleDiagram.vsdx", SaveFileFormat.Vsdx);
    }
}
