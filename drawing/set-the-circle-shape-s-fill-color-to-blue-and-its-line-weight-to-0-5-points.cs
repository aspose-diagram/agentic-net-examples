using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            // Create a new diagram
            Diagram diagram = new Diagram();

            // Get the first (default) page
            Page page = diagram.Pages[0];

            // Define circle (ellipse) parameters – a perfect circle with width = height = 2 inches
            double pinX = 5.0;   // X coordinate of the shape's center
            double pinY = 5.0;   // Y coordinate of the shape's center
            double size = 2.0;   // Diameter in inches

            // Add the circle shape to the page; DrawEllipse returns the shape ID (long)
            long shapeId = page.DrawEllipse(pinX, pinY, size, size);

            // Retrieve the shape object using the returned ID
            Shape circle = page.Shapes.GetShape(shapeId);

            // Set fill color to solid blue (#0000FF)
            circle.Fill.FillPattern.Value = 1;               // Solid fill pattern
            circle.Fill.FillForegnd.Value = "#0000FF";        // Blue foreground fill

            // Set line weight to 0.5 points (Visio stores line weight in inches;
            // 1 point = 1/72 inch, so 0.5 points = 0.5/72 inches)
            circle.Line.LineWeight.Value = 0.5 / 72.0;

            // Save the diagram to a VSDX file
            diagram.Save("CircleDiagram.vsdx", SaveFileFormat.Vsdx);
        }
    }