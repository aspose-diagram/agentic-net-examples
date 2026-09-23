using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Create a new empty Visio diagram (contains a default page)
            Diagram diagram = new Diagram();

            // Access the first (default) page – page index 0
            Page page = diagram.Pages[0];

            // Define shape parameters
            double pinX = 2.0;      // X coordinate of the shape's center (in inches)
            double pinY = 2.0;      // Y coordinate of the shape's center (in inches)
            double width = 2.0;     // Desired width of the rectangle (in inches)
            double height = 1.0;    // Height can be any value; using 1 inch here
            bool isCalculate = false; // Do not recalculate geometry automatically

            // Add a rectangle shape using the built‑in "Rectangle" master
            long rectShapeId = page.AddShape(pinX, pinY, width, height, "Rectangle", isCalculate);

            // Retrieve the shape object to modify its properties if needed
            Shape rectShape = page.Shapes.GetShape(rectShapeId);

            // Ensure the width is exactly 2 inches (redundant because we passed it above)
            rectShape.XForm.Width.Value = 2.0;

            // Save the diagram to a VSDX file
            diagram.Save("RectangleDiagram.vsdx", SaveFileFormat.Vsdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
