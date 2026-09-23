using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Create a new empty diagram
        Diagram diagram = new Diagram();

        // Access the first page (index 0)
        Page page = diagram.Pages[0];

        // Define grid layout parameters
        int rows = 5;                     // number of rows
        int cols = 4;                     // number of columns
        double shapeWidth = 1.0;          // width of each rectangle (in inches)
        double shapeHeight = 0.5;         // height of each rectangle (in inches)
        double hSpacing = 0.5;            // horizontal spacing between shapes (in inches)
        double vSpacing = 0.5;            // vertical spacing between shapes (in inches)

        // Starting position (center of first shape)
        double startX = shapeWidth / 2 + hSpacing;
        double startY = shapeHeight / 2 + vSpacing;

        // Add shapes in a grid
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                double pinX = startX + c * (shapeWidth + hSpacing);
                double pinY = startY + r * (shapeHeight + vSpacing);

                // Draw a rectangle and obtain its shape ID
                long shapeId = page.DrawRectangle(pinX, pinY, shapeWidth, shapeHeight);

                // Retrieve the shape object for further modifications
                Shape shape = page.Shapes.GetShape(shapeId);

                // Set the shape's text label (e.g., "R1C1")
                shape.Text.Value.Clear();
                shape.Text.Value.Add(new Txt($"R{r + 1}C{c + 1}"));
            }
        }

        // Configure PDF save options
        PdfSaveOptions pdfOptions = new PdfSaveOptions();
        pdfOptions.DefaultFont = "Arial";
        pdfOptions.SaveFormat = SaveFileFormat.Pdf;

        // Export the diagram as a PDF file
        diagram.Save("GridDiagram.pdf", pdfOptions);
    }
}
