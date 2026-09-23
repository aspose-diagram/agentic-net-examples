using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Create a new empty Visio diagram
            Diagram diagram = new Diagram();

            // Get the first (default) page
            Page page = diagram.Pages[0];

            // Define points for a diamond shape (center at (5,5), width 4, height 6)
            // Points are specified as a flat double array: x1, y1, x2, y2, ...
            double[] diamondPoints = new double[]
            {
                5, 8,   // Top vertex
                7, 5,   // Right vertex
                5, 2,   // Bottom vertex
                3, 5,   // Left vertex
                5, 8    // Close the polygon by returning to the top vertex
            };

            // Draw the diamond shape on the page
            long shapeId = page.DrawPolyline(diamondPoints);

            // Retrieve the shape object to modify its appearance
            Shape diamondShape = page.Shapes.GetShape(shapeId);

            // Set a thick border (line weight) – value is in inches
            diamondShape.Line.LineWeight.Value = 0.08; // Approx. 2 mm

            // Optionally set the border color
            diamondShape.Line.LineColor.Value = "#0000FF"; // Blue border

            // Export the diagram (containing the diamond) to SVG
            SVGSaveOptions svgOptions = new SVGSaveOptions();
            diagram.Save("diamond.svg", svgOptions);
        }
    }