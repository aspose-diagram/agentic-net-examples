using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Create a new diagram
        Diagram diagram = new Diagram();

        // Get the first page (index 0)
        Page page = diagram.Pages[0];

        // Define rectangle parameters
        double pinX = 2.0; // X coordinate (inches)
        double pinY = 2.0; // Y coordinate (inches)

        // Convert 3 centimeters to inches (1 cm = 0.393701 inches)
        double widthInInches = 3.0 * 0.393701;
        double heightInInches = widthInInches; // using same value for height

        // Draw the rectangle on the page
        long shapeId = page.DrawRectangle(pinX, pinY, widthInInches, heightInInches);

        // Save the diagram to a file
        diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
    }
}
