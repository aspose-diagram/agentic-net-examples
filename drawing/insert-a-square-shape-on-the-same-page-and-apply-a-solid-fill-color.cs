using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            // Create a new empty diagram
            using (Diagram diagram = new Diagram())
            {
                // Access the first (and only) page
                Page page = diagram.Pages[0];

                // Define square dimensions (width = height)
                double pinX = 2.0;   // X coordinate of the shape's pin (center)
                double pinY = 2.0;   // Y coordinate of the shape's pin (center)
                double size = 1.5;   // Width and height (in inches) for a square

                // Draw a square using DrawRectangle (returns the shape ID)
                long shapeId = page.DrawRectangle(pinX, pinY, size, size);

                // Retrieve the shape object using the returned ID
                Shape square = page.Shapes.GetShape(shapeId);

                // Apply a solid fill: set fill pattern to solid (1) and foreground color to red
                square.Fill.FillPattern.Value = 1;               // Solid fill pattern
                square.Fill.FillForegnd.Value = "#FF0000";       // Red color in hex

                // Save the diagram to a VSDX file
                diagram.Save("SquareShape.vsdx", SaveFileFormat.Vsdx);
            }
        }
    }