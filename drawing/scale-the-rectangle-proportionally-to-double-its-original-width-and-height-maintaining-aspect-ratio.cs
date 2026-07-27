using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vdx");

            // Access the first page (or any specific page)
            Page page = diagram.Pages[0];

            // Original rectangle parameters
            double pinX = 5.0;          // X coordinate of the rectangle's pin (center)
            double pinY = 5.0;          // Y coordinate of the rectangle's pin (center)
            double originalWidth = 2.0;
            double originalHeight = 1.0;

            // Scale factor to double width and height
            double scaleFactor = 2.0;

            // Compute new dimensions while preserving aspect ratio
            double newWidth = originalWidth * scaleFactor;
            double newHeight = originalHeight * scaleFactor;

            // Draw the rectangle with the scaled dimensions
            long shapeId = page.DrawRectangle(pinX, pinY, newWidth, newHeight);

            // (Optional) Adjust the shape's DropOnPageScale if you need to reflect scaling in the shape's properties
            // Shape shape = page.Shapes[shapeId];
            // shape.DropOnPageScale = new DoubleValue(scaleFactor * 100); // percentage

            // Save the modified diagram
            diagram.Save("output.vdx", SaveFileFormat.Vdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
