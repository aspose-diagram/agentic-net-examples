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
            string tempFile = Path.Combine(Path.GetTempPath(), "LineJumpStyleTest.vsdx");

            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Ensure there is at least one page
            Page page = diagram.Pages[0];

            // Add a simple rectangle shape (master name "Rectangle" exists in default stencils)
            // Parameters: pinX, pinY, width, height, master name, isCalculate (false)
            long shapeId = page.AddShape(2.0, 2.0, 2.0, 2.0, "Rectangle", false);

            // Retrieve the shape object
            Shape shape = page.Shapes.GetShape(shapeId);

            // Set the line jump style for the shape (using the layout sub‑object)
            shape.Layout.ConLineJumpStyle.Value = ConLineJumpStyleValue.Square;

            // Save the diagram to a file (VSDX format)
            diagram.Save(tempFile, SaveFileFormat.Vsdx);

            // Load the diagram back from the file
            Diagram loadedDiagram = new Diagram(tempFile);

            // Retrieve the same shape from the loaded diagram
            Page loadedPage = loadedDiagram.Pages[0];
            Shape loadedShape = loadedPage.Shapes.GetShape(shapeId);

            // Verify that the line jump style persisted
            if (loadedShape.Layout.ConLineJumpStyle.Value != ConLineJumpStyleValue.Square)
            {
                throw new Exception("Line jump style did not persist after serialization.");
            }
            else
            {
                Console.WriteLine("Line jump style persisted successfully.");
            }

            // Clean up temporary file
            try
            {
                File.Delete(tempFile);
            }
            catch
            {
                // Ignore any errors during cleanup
            }

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
