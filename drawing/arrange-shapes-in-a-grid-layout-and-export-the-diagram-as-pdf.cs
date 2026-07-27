using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class GridDiagramExample
{
    static void Main()
    {
        try
        {

            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Define grid parameters
            int rows = 5;
            int columns = 4;
            double shapeWidth = 1.0;   // inches
            double shapeHeight = 0.5;  // inches
            double horizontalSpacing = 0.5; // inches between shapes
            double verticalSpacing = 0.5;   // inches between shapes

            // Starting position (center of first shape)
            double startX = shapeWidth / 2;
            double startY = shapeHeight / 2;

            // Ensure the master shape exists; using built‑in "Rectangle" master
            string masterName = "Rectangle";

            // Add shapes in a grid layout
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < columns; col++)
                {
                    double pinX = startX + col * (shapeWidth + horizontalSpacing);
                    double pinY = startY + row * (shapeHeight + verticalSpacing);

                    // Add shape with specified position and size
                    diagram.AddShape(pinX, pinY, shapeWidth, shapeHeight, masterName, 0);
                }
            }

            // Save the diagram as PDF using PdfSaveOptions
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            diagram.Save("GridDiagram.pdf", pdfOptions);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
