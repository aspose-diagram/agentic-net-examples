using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Define temporary file path for the diagram
            string tempPath = Path.Combine(Path.GetTempPath(), "LineJumpStyleTest.vsdx");

            // Create a new diagram and add a simple line shape
            using (Diagram diagram = new Diagram())
            {
                // Ensure there is at least one page
                Page page = diagram.Pages[0];

                // Draw a line (1‑D shape) on the page
                // Parameters: startX, startY, endX, endY
                long lineId = page.DrawLine(1.0, 1.0, 5.0, 1.0);

                // Retrieve the shape object
                Shape lineShape = page.Shapes.GetShape((int)lineId);

                // Set the line jump style to Square (explicit jump style)
                lineShape.Layout.ConLineJumpStyle.Value = ConLineJumpStyleValue.Square;

                // Save the diagram to file
                diagram.Save(tempPath, SaveFileFormat.Vsdx);
            }

            // Reload the diagram from the saved file
            Diagram loadedDiagram = new Diagram(tempPath);
            Page loadedPage = loadedDiagram.Pages[0];
            // Retrieve the same shape by its ID
            Shape loadedLineShape = loadedPage.Shapes.GetShape((int)loadedPage.Shapes.GetShape(0).ID); // get first shape ID
            // Alternative: use the known ID (lineId) saved earlier; for simplicity we fetch the first shape
            // Verify that the line jump style persisted
            if (loadedLineShape.Layout.ConLineJumpStyle.Value != ConLineJumpStyleValue.Square)
            {
                throw new Exception("Line jump style did not persist after serialization.");
            }

            Console.WriteLine("Line jump style persisted successfully.");

        }
        catch (System.NullReferenceException ex)
        {
            Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
        }
    }
}
