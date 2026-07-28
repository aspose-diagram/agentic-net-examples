using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class VisioDuplicateAndExport
{
    static void Main()
    {
        try
        {

            // Load the existing Visio file
            var diagram = new Diagram("input.vsdx");

            // Get the first page (adjust index if needed)
            var page = diagram.Pages[0];

            // Identify the shape to duplicate (by its ID or Name)
            // Here we assume the shape has ID = 1; replace with the actual ID
            var originalShape = page.Shapes.GetShape(1);

            // Get the master name of the original shape (used for creating duplicates)
            string masterName = originalShape.Master.NameU;

            // Grid configuration
            int rows = 5;               // number of rows
            int columns = 4;            // number of columns
            double horizontalGap = 2.0; // gap between shapes in inches (X direction)
            double verticalGap = 1.5;   // gap between shapes in inches (Y direction)

            // Starting position (use original shape position as the top‑left corner)
            double startX = originalShape.XForm.PinX.Value;
            double startY = originalShape.XForm.PinY.Value;

            // Loop to create duplicates in a grid
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < columns; col++)
                {
                    // Skip the position of the original shape if it falls on the grid
                    if (row == 0 && col == 0) continue;

                    double newX = startX + col * horizontalGap;
                    double newY = startY - row * verticalGap; // Y decreases downwards in Visio

                    // Add a new shape based on the master at the calculated position
                    diagram.AddShape(newX, newY, masterName, page.ID);
                }
            }

            // Export the modified diagram to PDF
            diagram.Save("output.pdf", SaveFileFormat.Pdf);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
