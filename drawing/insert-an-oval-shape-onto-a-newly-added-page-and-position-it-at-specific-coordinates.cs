using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create a new diagram
        Diagram diagram = new Diagram();

        // Add a new page to the diagram
        Page newPage = new Page();
        diagram.Pages.Add(newPage);

        // Coordinates (in inches) where the oval will be placed
        double pinX = 5.0;   // X‑coordinate of the shape's pin (center)
        double pinY = 5.0;   // Y‑coordinate of the shape's pin (center)

        // Size of the oval (in inches)
        double width = 2.0;  // Width of the oval
        double height = 1.0; // Height of the oval

        // Draw the oval (ellipse) on the newly added page
        long shapeId = newPage.DrawEllipse(pinX, pinY, width, height);

        // Optional: set a name for the created shape
        Shape ovalShape = newPage.Shapes.GetShape(shapeId);
        ovalShape.Name = "MyOval";

        // Save the diagram to a file
        diagram.Save("Output.vsdx", SaveFileFormat.Vsdx);
    }
}
