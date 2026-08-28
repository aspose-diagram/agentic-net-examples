using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create a new empty diagram (contains a default first page)
        Diagram diagram = new Diagram();

        // Access the first page (index 0)
        Page page = diagram.Pages[0];

        // Position of the rectangle's pin (center) in inches
        double pinX = 2.0;
        double pinY = 2.0;

        // Width of 3 centimeters converted to inches (1 cm = 0.393701 inches)
        double widthCm = 3.0;
        double widthInches = widthCm * 0.393701;

        // Use the same value for height to create a square; adjust as needed
        double heightInches = widthInches;

        // Draw the rectangle on the page; returns the shape ID
        long shapeId = page.DrawRectangle(pinX, pinY, widthInches, heightInches);

        // Retrieve the shape object if further modifications are required
        Shape rectangleShape = page.Shapes.GetShape(shapeId);

        // Save the diagram to a VSDX file
        diagram.Save("RectangleDiagram.vsdx", SaveFileFormat.Vsdx);
    }
}
