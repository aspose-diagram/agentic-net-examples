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

            // Define triangle vertices (closed polygon) using a flat double array: x1, y1, x2, y2, ...
            double[] trianglePoints = new double[]
            {
                2.0, 2.0,   // Point A
                5.0, 2.0,   // Point B
                3.5, 5.0,   // Point C
                2.0, 2.0    // Close the shape by returning to Point A
            };

            // Draw the triangle; DrawPolyline returns the shape ID as a long
            long triangleId = page.DrawPolyline(trianglePoints);

            // Retrieve the shape object using the ID (cast to int for the indexer)
            Shape triangle = page.Shapes.GetShape((int)triangleId);

            // Set the line dash pattern to dashed
            triangle.Line.LinePattern.Value = LinePatternValue.Dash;

            // (Optional) Adjust line weight and color for better visibility
            triangle.Line.LineWeight.Value = 0.02;               // Thickness in inches
            triangle.Line.LineColor.Value = "#FF0000";          // Red color

            // Save the diagram as a PNG image
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
            diagram.Save("triangle.png", saveOptions);
        }
    }