using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Manipulation;

class Program
{
    static void Main()
    {
        try
        {

            // Temporary file path for the diagram
            string filePath = Path.Combine(Path.GetTempPath(), "LineJumpStyleTest.vsdx");

            // -------------------- Create diagram and set line jump style --------------------
            Diagram diagram = new Diagram();

            // Get the first (default) page
            Page page = diagram.Pages[0];

            // Draw two rectangle shapes
            long rect1Id = page.DrawRectangle(1.0, 1.0, 2.0, 1.0);
            long rect2Id = page.DrawRectangle(4.0, 1.0, 2.0, 1.0);

            // Connect the rectangles with a dynamic connector (connectorId = 0 creates a new one)
            page.ConnectShapesViaConnector(rect1Id, ConnectionPointPlace.Right, rect2Id, ConnectionPointPlace.Left, 0);

            // Locate the connector shape (OneD == true)
            Shape connector = null;
            foreach (Shape shape in page.Shapes)
            {
                if (shape.OneD)
                {
                    connector = shape;
                    break;
                }
            }

            if (connector == null)
                throw new Exception("Connector shape was not created.");

            // Set the line jump style to Square
            connector.Layout.ConLineJumpStyle.Value = ConLineJumpStyleValue.Square;

            // Save the diagram to a file
            diagram.Save(filePath, SaveFileFormat.Vsdx);

            // -------------------- Load diagram and verify persistence --------------------
            Diagram loadedDiagram = new Diagram(filePath);
            Page loadedPage = loadedDiagram.Pages[0];

            // Find the connector shape in the loaded diagram
            Shape loadedConnector = null;
            foreach (Shape shape in loadedPage.Shapes)
            {
                if (shape.OneD)
                {
                    loadedConnector = shape;
                    break;
                }
            }

            if (loadedConnector == null)
                throw new Exception("Connector shape was not found after loading.");

            // Verify that the line jump style persisted
            if (loadedConnector.Layout.ConLineJumpStyle.Value != ConLineJumpStyleValue.Square)
                throw new Exception("Line jump style did not persist after serialization.");

            Console.WriteLine("Line jump style persisted successfully.");

            // Clean up temporary file (optional)
            try
            {
                if (File.Exists(filePath))
                    File.Delete(filePath);
            }
            catch
            {
                // Ignore any cleanup errors
            }

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
