using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Load an existing Visio diagram (replace with your file path)
        Diagram diagram = new Diagram("input.vsdx");

        // Define rectangle parameters
        double pinX = 2.0;      // X coordinate of the rectangle's center (in inches)
        double pinY = 3.0;      // Y coordinate of the rectangle's center (in inches)
        double width = 1.5;     // Width of the rectangle (in inches)
        double height = 1.0;    // Height of the rectangle (in inches)
        string masterName = "Rectangle"; // Master name for a rectangle shape
        int pageNumber = 0;     // Index of the page (0 = first page)

        // Add the rectangle shape to the specified page using the master
        long shapeId = diagram.AddShape(pinX, pinY, width, height, masterName, pageNumber);

        // Save the modified diagram (replace with your desired output path)
        diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
    }
}
