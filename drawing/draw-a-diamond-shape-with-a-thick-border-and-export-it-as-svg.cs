using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing;

class Program
{
    static void Main(string[] args)
    {
        // Define output SVG file path
        string outputPath = "diamond.svg";

        try
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Ensure there is at least one page; the default constructor creates one
            Page page = diagram.Pages[0];

            // Add a diamond shape to the page.
            // Parameters: pinX, pinY (position), master name, isCalculate flag.
            long shapeId = page.AddShape(5.0, 5.0, "Diamond", false);

            // Retrieve the created shape using its ID
            Shape diamond = page.Shapes.GetShape(shapeId);

            // Set a thick border (line weight). Value is in points.
            diamond.Line.LineWeight.Value = 4.0;

            // Optionally ensure the line pattern is solid
            diamond.Line.LinePattern.Value = LinePatternValue.Solid;

            // Save the diagram as SVG
            diagram.Save(outputPath, SaveFileFormat.Svg);
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}