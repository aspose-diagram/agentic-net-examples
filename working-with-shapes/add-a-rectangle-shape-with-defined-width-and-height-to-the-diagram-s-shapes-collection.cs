using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Create a new diagram (or load an existing one if needed)
        Diagram diagram = new Diagram();

        // Access the first page of the diagram
        Page page = diagram.Pages[0];

        // Define rectangle parameters (position and size in inches)
        double pinX = 5.0;    // X‑coordinate of the rectangle's pin (center of rotation)
        double pinY = 5.0;    // Y‑coordinate of the rectangle's pin (center of rotation)
        double width = 2.0;  // Width of the rectangle
        double height = 1.0; // Height of the rectangle

        // Add the rectangle shape to the page's Shapes collection
        // DrawRectangle returns the unique shape ID within the page
        long shapeId = page.DrawRectangle(pinX, pinY, width, height);

        // (Optional) Retrieve the shape object if further modifications are required
        // Shape rectangleShape = page.Shapes.GetShape(shapeId);

        // Save the diagram to a file
        diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
    }
}
