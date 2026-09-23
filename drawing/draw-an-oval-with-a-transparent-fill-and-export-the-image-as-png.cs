using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Create a new empty diagram
        Diagram diagram = new Diagram();

        // Get the first (default) page
        Page page = diagram.Pages[0];

        // Define oval position and size (in inches)
        double pinX = 5.0;   // center X
        double pinY = 5.0;   // center Y
        double width = 4.0;  // horizontal diameter
        double height = 2.0; // vertical diameter

        // Draw the oval (ellipse) on the page
        long shapeId = page.DrawEllipse(pinX, pinY, width, height);

        // Retrieve the shape object
        Shape ovalShape = page.Shapes.GetShape((int)shapeId);

        // Set the fill pattern to 'None' for transparent fill
        ovalShape.Fill.FillPattern.Value = 0; // 0 = No fill (transparent)

        // Export the diagram as a PNG image
        ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);
        diagram.Save("output.png", pngOptions);
    }
}
