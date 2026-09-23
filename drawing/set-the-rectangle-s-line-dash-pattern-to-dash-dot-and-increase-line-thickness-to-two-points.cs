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

        // Get the first (default) page
        Page page = diagram.Pages[0];

        // Add a rectangle shape to the page
        // Parameters: pinX, pinY, width, height (all in inches)
        double pinX = 5.0;   // horizontal position
        double pinY = 5.0;   // vertical position
        double width = 2.0;  // rectangle width
        double height = 1.0; // rectangle height
        long rectId = page.DrawRectangle(pinX, pinY, width, height);

        // Retrieve the shape object using the returned ID
        Shape rectangle = page.Shapes.GetShape((int)rectId);

        // Set line dash pattern to DashDot
        rectangle.Line.LinePattern.Value = LinePatternValue.DashDot;

        // Set line thickness to 2 points (2/72 inches)
        rectangle.Line.LineWeight.Value = 2.0 / 72.0;

        // Save the diagram to a VSDX file
        string outputPath = "RectangleStyled.vsdx";
        diagram.Save(outputPath, SaveFileFormat.Vsdx);

        Console.WriteLine($"Diagram saved to {outputPath}");
    }
}
