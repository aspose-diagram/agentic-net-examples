using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create a new blank diagram
        using (Diagram diagram = new Diagram())
        {
            // Access the first (default) page
            Page page = diagram.Pages[0];

            // Retrieve page dimensions (in inches)
            double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

            // Define rectangle size
            double rectWidth = 2.0;   // inches
            double rectHeight = 1.0;  // inches

            // Calculate equal horizontal spacing (including margins)
            double spacing = (pageWidth - 3 * rectWidth) / 4.0;

            // Vertical center position for all rectangles
            double pinY = pageHeight / 2.0;

            // Add three rectangles with equal spacing
            for (int i = 0; i < 3; i++)
            {
                double pinX = spacing + rectWidth / 2.0 + i * (rectWidth + spacing);
                long shapeId = page.DrawRectangle(pinX, pinY, rectWidth, rectHeight);

                // Optional: add a label to each rectangle
                Shape shape = page.Shapes.GetShape(shapeId);
                shape.Text.Value.Clear();
                shape.Text.Value.Add(new Txt($"Rect {i + 1}"));
            }

            // Save the diagram to a VSDX file
            diagram.Save("Rectangles.vsdx", SaveFileFormat.Vsdx);
        }
    }
}
