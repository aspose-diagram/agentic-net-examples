using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Create a new empty diagram
        using (Diagram diagram = new Diagram())
        {
            // Access the first page of the diagram
            Page page = diagram.Pages[0];

            // Define grid layout parameters
            int rows = 3;                     // Number of rows
            int cols = 4;                     // Number of columns
            double shapeWidth = 1.0;          // Width of each rectangle (in inches)
            double shapeHeight = 0.5;         // Height of each rectangle (in inches)
            double hSpacing = 0.5;            // Horizontal spacing between shapes (in inches)
            double vSpacing = 0.5;            // Vertical spacing between shapes (in inches)

            // Starting position (center of first shape)
            double startX = shapeWidth / 2 + hSpacing;
            double startY = shapeHeight / 2 + vSpacing;

            // Create shapes arranged in a grid
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    double pinX = startX + c * (shapeWidth + hSpacing);
                    double pinY = startY + r * (shapeHeight + vSpacing);

                    // Draw a rectangle at the calculated position
                    long shapeId = page.DrawRectangle(pinX, pinY, shapeWidth, shapeHeight);

                    // Retrieve the shape to set its text
                    Shape shape = page.Shapes.GetShape(shapeId);
                    shape.Text.Value.Clear();
                    shape.Text.Value.Add(new Txt($"R{r + 1}C{c + 1}"));
                }
            }

            // Export the diagram to PDF
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial";
            diagram.Save("GridDiagram.pdf", pdfOptions);
        }
    }
}
