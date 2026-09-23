using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Access the first page of the diagram
            Aspose.Diagram.Page page = diagram.Pages[0];

            // Define the position (PinX, PinY) and size for the square
            double pinX = 2.0;
            double pinY = 2.0;
            double size = 1.0; // Width and height are equal for a square

            // Draw the square using DrawRectangle (width = height)
            long shapeId = page.DrawRectangle(pinX, pinY, size, size);

            // Retrieve the shape object using its ID
            Shape squareShape = page.Shapes.GetShape(shapeId);

            // Apply a solid fill: set fill pattern to solid (1) and set foreground color
            squareShape.Fill.FillPattern.Value = 1;
            squareShape.Fill.FillForegnd.Value = "#FF0000"; // solid red fill

            // Save the diagram to a VSDX file
            diagram.Save("SquareDiagram.vsdx", SaveFileFormat.Vsdx);
        }
    }