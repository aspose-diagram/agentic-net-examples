using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Create a new diagram (using the provided creation rule)
            Aspose.Diagram.Diagram diagram = new Aspose.Diagram.Diagram();

            // Access the first page (page index is 0)
            Aspose.Diagram.Page page = diagram.Pages[0];

            // Define position for the rectangle's pin (center). Adjust as needed.
            double pinX = 2.0;   // inches from the left edge of the page
            double pinY = 2.0;   // inches from the top edge of the page

            // Define rectangle dimensions
            double widthInches = 2.0;   // required width
            double heightInches = 1.0;  // arbitrary height (can be changed)

            // Add a rectangle shape using the built‑in "Rectangle" master on page 1
            // (AddShape with pinX, pinY, width, height, masterName, pageNumber)
            long shapeId = diagram.AddShape(pinX, pinY, widthInches, heightInches, "Rectangle", 1);

            // Save the diagram (using the provided saving rule)
            diagram.Save("RectangleDiagram.vdx", Aspose.Diagram.SaveFileFormat.Vdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
