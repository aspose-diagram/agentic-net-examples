using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Create a new diagram instance
        Diagram diagram = new Diagram();

        // Use the first page of the diagram
        Page page = diagram.Pages[0];

        // Rectangle parameters (in inches)
        double pinX = 5.0;   // X‑coordinate of the rectangle's pin (center)
        double pinY = 5.0;   // Y‑coordinate of the rectangle's pin (center)
        double width = 2.0;  // Width of the rectangle
        double height = 1.0; // Height of the rectangle

        // Add the rectangle shape to the page's Shapes collection
        long shapeId = page.DrawRectangle(pinX, pinY, width, height);

        // (Optional) Access the newly added shape if further modifications are needed
        // Shape rectShape = diagram.Pages[0].Shapes[shapeId];

        // Save the diagram to a VDX file
        diagram.Save("RectangleDiagram.vdx", SaveFileFormat.Vdx);
    }
}
