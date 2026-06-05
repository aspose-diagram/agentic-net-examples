using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            // Create a new empty Visio diagram
            using (Diagram diagram = new Diagram())
            {
                // Get the first (default) page
                Page page = diagram.Pages[0];

                // Draw a square using DrawRectangle (width == height)
                // Parameters: pinX, pinY, width, height
                // The pin point is the center of the shape.
                double pinX = 2.0;   // X coordinate of the square center
                double pinY = 2.0;   // Y coordinate of the square center
                double size = 1.0;   // Width and height (square)

                long shapeId = page.DrawRectangle(pinX, pinY, size, size);

                // Retrieve the shape object using the returned ID
                Shape squareShape = page.Shapes.GetShape(shapeId);

                // Apply a solid fill: set fill pattern to solid (1) and foreground color
                squareShape.Fill.FillPattern.Value = 1;               // Solid fill pattern
                squareShape.Fill.FillForegnd.Value = "#00FF00";       // Green color in HEX

                // Save the diagram to a VSDX file
                diagram.Save("SquareShapeOutput.vsdx", SaveFileFormat.Vsdx);
            }
        }
    }