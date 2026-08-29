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

        // Access the first (default) page
        Page page = diagram.Pages[0];

        // Draw a simple rectangle shape
        // Parameters: PinX, PinY, Width, Height (all in inches)
        long shapeIdLong = page.DrawRectangle(1.0, 1.0, 2.0, 1.0);
        int shapeId = (int)shapeIdLong;

        // Retrieve the shape object
        Shape shape = page.Shapes.GetShape(shapeId);

        // Set a solid fill pattern and a foreground color
        shape.Fill.FillPattern.Value = 1;               // Solid fill
        shape.Fill.FillForegnd.Value = "#FF0000";       // Red color

        // Verify that the shape's own fill values are set correctly
        if (shape.Fill.FillForegnd.Value != "#FF0000")
            throw new Exception("Initial fill color not set correctly.");

        // Save the diagram to a temporary file
        string tempPath = "FillInheritanceTest.vsdx";
        diagram.Save(tempPath, SaveFileFormat.Vsdx);

        // Load the diagram back from the file
        Diagram loadedDiagram = new Diagram(tempPath);
        Page loadedPage = loadedDiagram.Pages[0];
        Shape loadedShape = loadedPage.Shapes.GetShape(shapeId);

        // Verify that the fill color persisted after reload
        if (loadedShape.Fill.FillForegnd.Value != "#FF0000")
            throw new Exception("Fill color was not persisted after reload.");

        // Verify that the inherited fill matches the explicit fill
        // According to Aspose.Diagram, matching values indicate inheritance is applied
        if (loadedShape.InheritFill.FillForegnd.Value != loadedShape.Fill.FillForegnd.Value)
            throw new Exception("Fill inheritance is not reported correctly after reload.");

        Console.WriteLine("Fill inheritance verification passed successfully.");
    }
}
