using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Define output file path
        string outputPath = "CircleDiagram.vsdx";

        // Define circle parameters in centimeters
        double centerXcm = 5.0;      // X coordinate of the circle center
        double centerYcm = 7.0;      // Y coordinate of the circle center
        double diameterCm = 3.0;     // Diameter of the circle

        // Convert centimeters to inches (Aspose.Diagram uses inches for geometry)
        const double cmPerInch = 2.54;
        double centerXinch = centerXcm / cmPerInch;
        double centerYinch = centerYcm / cmPerInch;
        double diameterInch = diameterCm / cmPerInch;

        try
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Get the first (default) page
            Page page = diagram.Pages[0];

            // Draw a circle (ellipse with equal width and height) at the specified position
            // DrawEllipse returns the shape ID (long)
            long circleId = page.DrawEllipse(centerXinch, centerYinch, diameterInch, diameterInch);

            // Optional: retrieve the shape object if further modifications are needed
            Shape circleShape = page.Shapes.GetShape(circleId);
            // Example: set the shape's line color to black (optional)
            circleShape.Line.LineColor.Value = "#000000";

            // Save the diagram to a VSDX file
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}