using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Create a new empty diagram
        var diagram = new Diagram();

        // Get the first page (a new diagram always contains at least one page)
        Page page = diagram.Pages[0];

        // Draw a simple rectangle shape on the page
        long shapeIdLong = page.DrawRectangle(1.0, 1.0, 2.0, 2.0);
        int shapeId = (int)shapeIdLong;
        Shape shape = page.Shapes.GetShape(shapeId);

        // Create a style sheet that defines a solid red fill
        var style = new StyleSheet();
        style.ID = diagram.StyleSheets.Count + 1;
        style.Name = "RedFillStyle";
        style.Fill.FillPattern.Value = 1;               // Solid fill
        style.Fill.FillForegnd.Value = "#FF0000";       // Red color

        // Add the style sheet to the diagram
        diagram.StyleSheets.Add(style);

        // Apply the style sheet to the shape so the shape inherits the fill
        shape.FillStyle = style;

        // Save the diagram to a temporary file
        string tempPath = "temp.vsdx";
        diagram.Save(tempPath, SaveFileFormat.Vsdx);

        // Load the diagram back from the file
        var loadedDiagram = new Diagram(tempPath);
        Page loadedPage = loadedDiagram.Pages[0];
        Shape loadedShape = loadedPage.Shapes.GetShape(shapeId);

        // Retrieve the inherited fill foreground color after reload
        string inheritedFillColor = loadedShape.InheritFill.FillForegnd.Value;
        string expectedFillColor = style.Fill.FillForegnd.Value;

        // Verify that the inherited fill matches the style's fill
        if (!string.Equals(inheritedFillColor, expectedFillColor, StringComparison.OrdinalIgnoreCase))
        {
            throw new Exception($"Fill inheritance verification failed. Expected: {expectedFillColor}, Actual: {inheritedFillColor}");
        }

        Console.WriteLine("Fill inheritance verified successfully after save and reload.");
    }
}
