using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Create a new blank diagram
            using (Diagram diagram = new Diagram())
            {
                // Get the first (default) page
                Page page = diagram.Pages[0];

                // Page dimensions (in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Define rectangle size
                double rectWidth = 2.0;   // inches
                double rectHeight = 1.0;  // inches

                // Calculate equal spacing (including left/right margins)
                double totalRectWidth = 3 * rectWidth;
                double remainingSpace = pageWidth - totalRectWidth;
                if (remainingSpace < 0)
                    throw new Exception("Rectangles are too wide for the page.");

                double spacing = remainingSpace / 4.0; // left, between, between, right

                // Vertical position (centered vertically)
                double pinY = pageHeight / 2.0;

                // Add three rectangles with calculated horizontal positions
                for (int i = 0; i < 3; i++)
                {
                    double pinX = (i + 1) * spacing + (i + 0.5) * rectWidth;
                    long shapeId = page.AddShape(pinX, pinY, rectWidth, rectHeight, "Rectangle");
                    // Retrieve the shape if further modifications are needed
                    Shape shape = page.Shapes.GetShape(shapeId);
                    // Optional: set a simple label
                    shape.Text.Value.Clear();
                    shape.Text.Value.Add(new Txt($"Rect {i + 1}"));
                }

                // Save the diagram as VSDX
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
            }

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
