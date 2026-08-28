using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Create a new empty Visio diagram
        using (Diagram diagram = new Diagram())
        {
            // Add a new page to the diagram
            Page page = new Page();
            diagram.Pages.Add(page);

            // Draw an ellipse (circle) with arbitrary size at the origin
            // Width and height are equal to create a circle
            double pinX = 0;      // X coordinate of the shape's pin (center)
            double pinY = 0;      // Y coordinate of the shape's pin (center)
            double size = 2.0;    // Diameter of the circle (in drawing units)
            page.DrawEllipse(pinX, pinY, size, size);

            // Center all shapes on the page (the circle will be centered)
            page.CenterDrawing();

            // Save the diagram to a VDX file
            diagram.Save("CenteredCircle.vdx", SaveFileFormat.Vdx);
        }
    }
}
