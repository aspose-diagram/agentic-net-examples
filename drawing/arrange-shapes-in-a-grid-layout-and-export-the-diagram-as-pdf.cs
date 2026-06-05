using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Grid configuration
            int rows = 5;                     // number of rows
            int cols = 4;                     // number of columns
            double shapeWidth = 1.0;          // width of each shape (in inches)
            double shapeHeight = 0.5;         // height of each shape (in inches)
            double spacing = 0.2;             // space between shapes (in inches)

            // Starting coordinates (center of first shape)
            double startX = shapeWidth / 2;
            double startY = shapeHeight / 2;

            // Add shapes to the diagram in a grid pattern
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    double pinX = startX + c * (shapeWidth + spacing);
                    double pinY = startY + r * (shapeHeight + spacing);

                    // Add a rectangle shape (master name "Rectangle") at calculated position
                    diagram.AddShape(pinX, pinY, "Rectangle", 0);
                }
            }

            // Apply automatic layout (optional – can be omitted if manual positioning is sufficient)
            LayoutOptions layoutOptions = new LayoutOptions();
            // If a grid layout style is desired and supported, set it here, e.g.:
            // layoutOptions.LayoutStyle = LayoutStyle.Grid;
            diagram.Layout(layoutOptions);

            // Save the diagram as a PDF file
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            diagram.Save("GridDiagram.pdf", pdfOptions);

            // Release resources
            diagram.Dispose();

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
