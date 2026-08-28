using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Create a new empty diagram
            using (Diagram diagram = new Diagram())
            {
                // Ensure there is at least one page
                if (diagram.Pages.Count == 0)
                {
                    diagram.Pages.Add(new Page());
                }

                // Access the first page (page index 0)
                Page page = diagram.Pages[0];

                // Define position and size for the rectangle
                double pinX = 2.0;   // X coordinate of the shape's pin (center) in inches
                double pinY = 2.0;   // Y coordinate of the shape's pin (center) in inches
                double width = 2.0;  // Desired width in inches
                double height = 1.0; // Arbitrary height (can be adjusted as needed)

                // Add a rectangle shape using the "Rectangle" master
                long shapeId = page.AddShape(pinX, pinY, width, height, "Rectangle");

                // Retrieve the shape to modify its properties
                Shape rectShape = page.Shapes.GetShape(shapeId);

                // Ensure the width is set to 2 inches (already set via AddShape, but reaffirmed here)
                rectShape.XForm.Width.Value = 2.0;

                // Optional: save the diagram to verify the shape was added
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
            }

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
