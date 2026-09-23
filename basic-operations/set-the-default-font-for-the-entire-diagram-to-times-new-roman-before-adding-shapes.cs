using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Configure the global default font before any shapes are added
        FontConfigs.DefaultFontName = "Times New Roman";

        // Create a new empty diagram
        Diagram diagram = new Diagram();

        // Access the first page (automatically created with a new diagram)
        Page page = diagram.Pages[0];

        // Add a rectangle shape using the DrawRectangle method
        double pinX = 2.0;   // X coordinate of the shape's center
        double pinY = 2.0;   // Y coordinate of the shape's center
        double width = 2.0; // Width of the rectangle
        double height = 1.0; // Height of the rectangle
        long shapeId = page.DrawRectangle(pinX, pinY, width, height);

        // Retrieve the shape object to modify its properties
        Shape shape = page.Shapes.GetShape(shapeId);

        // Set the shape's text
        shape.Text.Value.Clear();
        shape.Text.Value.Add(new Txt("Sample Text"));

        // Save the diagram to a VSDX file
        diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
    }
}
