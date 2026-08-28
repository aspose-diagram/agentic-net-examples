using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

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
                    diagram.Pages.Add(new Page());

                // Get the first page
                Page page = diagram.Pages[0];

                // Page dimensions (in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Rectangle size (in inches)
                double rectWidth = 1.0;
                double rectHeight = 0.5;

                // Calculate equal horizontal spacing (including margins)
                double spacing = (pageWidth - 3 * rectWidth) / 4.0;

                // Vertical position (center of the page)
                double pinY = pageHeight / 2.0;

                // Add three rectangles with equal spacing
                for (int i = 0; i < 3; i++)
                {
                    double pinX = spacing + rectWidth / 2.0 + i * (rectWidth + spacing);
                    long shapeId = page.AddShape(pinX, pinY, rectWidth, rectHeight, "Rectangle");
                    // Retrieve the shape if further modifications are needed
                    Shape shape = page.Shapes.GetShape(shapeId);
                    // Optional: add a label to each rectangle
                    shape.Text.Value.Clear();
                    shape.Text.Value.Add(new Txt($"Rect {i + 1}"));
                }

                // Save the diagram as VSDX
                diagram.Save("ThreeRectangles.vsdx", SaveFileFormat.Vsdx);
            }

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
