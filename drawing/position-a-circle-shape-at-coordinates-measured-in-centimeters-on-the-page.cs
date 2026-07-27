using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            // Create a new empty Visio diagram
            using (Diagram diagram = new Diagram())
            {
                // Access the first (default) page
                Page page = diagram.Pages[0];

                // Desired position and size in centimeters
                double cmPinX = 5.0;      // X coordinate of the circle center
                double cmPinY = 3.0;      // Y coordinate of the circle center
                double cmDiameter = 2.0; // Diameter of the circle

                // Convert centimeters to inches (Aspose.Diagram uses inches)
                const double cmToInch = 0.393700787;
                double pinXInches = cmPinX * cmToInch;
                double pinYInches = cmPinY * cmToInch;
                double diameterInches = cmDiameter * cmToInch;

                // Draw an ellipse (circle) on the page
                // DrawEllipse returns the shape ID (long)
                long shapeId = page.DrawEllipse(pinXInches, pinYInches, diameterInches, diameterInches);

                // Retrieve the shape to optionally modify its appearance
                Shape circleShape = page.Shapes.GetShape((int)shapeId);
                // Example: set the fill color to red
                circleShape.Fill.FillForegnd.Value = "#FF0000";

                // Save the diagram to a VSDX file
                diagram.Save("CircleDiagram.vsdx", SaveFileFormat.Vsdx);
            }
        }
    }