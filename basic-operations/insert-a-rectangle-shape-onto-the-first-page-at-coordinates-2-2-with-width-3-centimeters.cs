using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Create a new diagram (empty Visio file)
            Diagram diagram = new Diagram();

            // Get the first page (index 0)
            Page page = diagram.Pages[0];

            // Define position (PinX, PinY) in inches
            double pinX = 2.0; // X coordinate
            double pinY = 2.0; // Y coordinate

            // Width is 3 centimeters; convert to inches (1 inch = 2.54 cm)
            double widthCm = 3.0;
            double widthIn = widthCm / 2.54;

            // For this example, set height equal to width (square). Adjust as needed.
            double heightIn = widthIn;

            // Add a rectangle shape using the built‑in "Rectangle" master
            long shapeId = page.AddShape(pinX, pinY, widthIn, heightIn, "Rectangle");

            // Optionally, you can work with the shape via its ID:
            // Shape rectShape = page.Shapes[shapeId];

            // Save the diagram to a VSDX file
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
