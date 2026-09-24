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

            // Define temporary file path
            string tempPath = Path.Combine(Path.GetTempPath(), "LineJumpStyleTest.vsdx");

            try
            {
                // ---------- Create diagram ----------
                Diagram diagram = new Diagram();

                // Ensure there is at least one page
                if (diagram.Pages.Count == 0)
                {
                    // Page IDs must be unique; start with 1
                    diagram.Pages.Add(new Page(1));
                }

                // Use the first page
                Page page = diagram.Pages[0];

                // Add a dynamic connector shape (master name must exist in the default stencil)
                // The last parameter 'false' indicates that the shape size is not calculated automatically
                long connectorId = page.AddShape(2.0, 2.0, "Dynamic connector", false);

                // Retrieve the connector shape
                Shape connector = page.Shapes.GetShape(connectorId);

                // Set the line jump style to Arc
                connector.Layout.ConLineJumpStyle.Value = ConLineJumpStyleValue.Arc;

                // Save the diagram to a file (VSDX format)
                diagram.Save(tempPath, SaveFileFormat.Vsdx);

                // ---------- Load diagram ----------
                Diagram loadedDiagram = new Diagram(tempPath);

                // Retrieve the same page and shape by ID
                Page loadedPage = loadedDiagram.Pages[0];
                Shape loadedConnector = loadedPage.Shapes.GetShape(connectorId);

                // Verify that the line jump style persisted
                if (loadedConnector.Layout.ConLineJumpStyle.Value != ConLineJumpStyleValue.Arc)
                {
                    throw new Exception("Line jump style did not persist after serialization.");
                }

                Console.WriteLine("Line jump style persisted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Test failed: {ex.Message}");
                throw;
            }
            finally
            {
                // Clean up temporary file
                if (File.Exists(tempPath))
                {
                    try { File.Delete(tempPath); } catch { /* ignore cleanup errors */ }
                }
            }

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
