using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Create a new diagram (or load an existing one if needed)
            Diagram diagram = new Diagram();

            // Access the first page of the diagram
            Page page = diagram.Pages[0];

            // Starting position for the first triangle (in inches)
            double startX = 5.0;
            double startY = 5.0;

            // Distance between each triangle vertically
            double verticalSpacing = 2.0;

            // Add the first triangle shape
            long shapeId1 = page.AddShape(startX, startY, "Triangle");

            // Add the second triangle shape directly below the first
            long shapeId2 = page.AddShape(startX, startY + verticalSpacing, "Triangle");

            // Add the third triangle shape directly below the second
            long shapeId3 = page.AddShape(startX, startY + 2 * verticalSpacing, "Triangle");

            // Save the diagram to a file
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
