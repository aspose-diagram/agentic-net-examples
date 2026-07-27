using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Create a new diagram instance
        Diagram diagram = new Diagram();

        // Add a new page to the diagram
        Page page = new Page();
        diagram.Pages.Add(page);

        // Coordinates (in inches) where the oval will be placed
        double pinX = 5.0;   // X‑coordinate of the shape's pin (center)
        double pinY = 7.0;   // Y‑coordinate of the shape's pin (center)

        // Size of the oval (in inches)
        double width = 3.0;  // Width of the oval
        double height = 2.0; // Height of the oval

        // Draw the oval (ellipse) on the newly added page
        long ovalShapeId = page.DrawEllipse(pinX, pinY, width, height);

        // Optional: set a friendly name for the shape
        Shape ovalShape = page.Shapes.GetShape(ovalShapeId);
        ovalShape.Name = "MyOval";

        // Save the diagram to a VDX file
        diagram.Save("Output.vdx", SaveFileFormat.Vdx);
    }
}
