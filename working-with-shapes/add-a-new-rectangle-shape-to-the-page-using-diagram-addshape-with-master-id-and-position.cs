using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Create a new diagram (uses the provided creation rule)
            Diagram diagram = new Diagram();

            // Define position for the rectangle (in inches)
            double pinX = 2.0; // X‑coordinate of the shape's pin (center)
            double pinY = 2.0; // Y‑coordinate of the shape's pin (center)

            // Master name for a rectangle shape in Visio stencil
            string masterName = "Rectangle";

            // Page index (0 = first page)
            int pageNumber = 0;

            // Add the rectangle shape to the specified page using the master
            // This utilizes the Diagram.AddShape(double, double, string, int) overload
            long shapeId = diagram.AddShape(pinX, pinY, masterName, pageNumber);

            // (Optional) You can further manipulate the shape via its ID if needed
            // Shape rectShape = diagram.Pages[pageNumber].Shapes[shapeId];
            // rectShape.SetWidth(3.0);
            // rectShape.SetHeight(2.0);

            // Save the diagram (uses the provided save rule)
            diagram.Save("output.vdx", SaveFileFormat.Vdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
