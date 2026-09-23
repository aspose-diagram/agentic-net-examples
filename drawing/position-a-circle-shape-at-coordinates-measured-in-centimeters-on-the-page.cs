using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Coordinates and size in centimeters
        double cmX = 5.0;      // center X position
        double cmY = 3.0;      // center Y position
        double cmWidth = 2.0;  // width of the circle
        double cmHeight = 2.0; // height of the circle (same as width for a perfect circle)

        // Conversion factor from centimeters to inches (Aspose.Diagram uses inches)
        const double cmToInch = 0.393700787;

        // Convert to inches for the API
        double pinX = cmX * cmToInch;
        double pinY = cmY * cmToInch;
        double width = cmWidth * cmToInch;
        double height = cmHeight * cmToInch;

        // Create a new diagram (contains a default page)
        using (Diagram diagram = new Diagram())
        {
            // Get the first (default) page
            Page page = diagram.Pages[0];

            // Draw an ellipse (circle) at the specified position and size
            long shapeId = page.DrawEllipse(pinX, pinY, width, height);

            // Retrieve the shape if further modifications are needed
            Shape circle = page.Shapes.GetShape((int)shapeId);

            // Example: set a fill color (optional)
            circle.Fill.FillForegnd.Value = "#FF0000"; // red fill

            // Save the diagram to a VSDX file
            diagram.Save("circle.vsdx", SaveFileFormat.Vsdx);
        }
    }
}
