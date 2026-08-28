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

        // Ensure there is at least one page to work with
        Page page;
        if (diagram.Pages.Count == 0)
        {
            page = new Page();
            diagram.Pages.Add(page);
        }
        else
        {
            page = diagram.Pages[0];
        }

        // Define square position and size (in inches)
        double pinX = 2.0;   // X coordinate of the square's center
        double pinY = 2.0;   // Y coordinate of the square's center
        double size = 1.0;   // Width and height (square)

        // Draw a square using the DrawRectangle method (width = height)
        long shapeId = page.DrawRectangle(pinX, pinY, size, size);

        // Retrieve the shape object by its ID
        Shape square = page.Shapes.GetShape(shapeId);

        // Apply a solid fill pattern (1) and set the fill color to red
        square.Fill.FillPattern.Value = 1;          // Solid fill
        square.Fill.FillForegnd.Value = "#FF0000"; // Red color in hex

        // Save the diagram to a VSDX file
        diagram.Save("SquareDiagram.vsdx", SaveFileFormat.Vsdx);
    }
}
