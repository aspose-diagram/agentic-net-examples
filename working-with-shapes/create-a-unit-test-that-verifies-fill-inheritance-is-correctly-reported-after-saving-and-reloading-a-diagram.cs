using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Create a new diagram
        Diagram diagram = new Diagram();

        // Get the first page (there is always at least one page in a new diagram)
        Page page = diagram.Pages[0];

        // Draw a rectangle shape (pinX, pinY, width, height)
        long shapeIdLong = page.DrawRectangle(2.0, 2.0, 2.0, 2.0);
        int shapeId = (int)shapeIdLong;

        // Retrieve the shape object
        Shape shape = page.Shapes.GetShape(shapeId);

        // Set a solid fill pattern and foreground color
        shape.Fill.FillPattern.Value = 1;               // Solid fill
        shape.Fill.FillForegnd.Value = "#FF0000";       // Red color

        // Save the diagram to a temporary file
        string tempPath = "temp_diagram.vsdx";
        diagram.Save(tempPath, SaveFileFormat.Vsdx);

        // Reload the diagram from the saved file
        Diagram loadedDiagram = new Diagram(tempPath);
        Page loadedPage = loadedDiagram.Pages[0];
        Shape loadedShape = loadedPage.Shapes.GetShape(shapeId);

        // Verify that the fill color is preserved after reload
        if (!string.Equals(loadedShape.Fill.FillForegnd.Value, "#FF0000", StringComparison.OrdinalIgnoreCase))
        {
            throw new Exception("Fill foreground color was not preserved after reload.");
        }

        // Verify that the inherited fill matches the explicit fill (since we set it directly)
        if (!string.Equals(loadedShape.InheritFill.FillForegnd.Value, loadedShape.Fill.FillForegnd.Value, StringComparison.OrdinalIgnoreCase))
        {
            throw new Exception("Inherited fill does not match explicit fill after reload.");
        }

        Console.WriteLine("Fill inheritance verification passed successfully.");
    }
}
