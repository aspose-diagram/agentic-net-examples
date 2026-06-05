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

            // Define circle parameters
            int circleCount = 5;
            double radius = 1.0; // inches
            double startX = 2.0;
            double startY = 2.0;
            double spacing = 2.5; // distance between circle centers

            // Simple color palette (hex strings)
            string[] colors = new string[]
            {
                "#FF0000", // Red
                "#00FF00", // Green
                "#0000FF", // Blue
                "#FFFF00", // Yellow
                "#FF00FF"  // Magenta
            };

            for (int i = 0; i < circleCount; i++)
            {
                // Calculate position for each circle
                double pinX = startX + i * spacing;
                double pinY = startY;

                // Draw an ellipse (circle) on the page
                // DrawEllipse(pinX, pinY, width, height)
                long shapeId = page.DrawEllipse(pinX, pinY, radius * 2, radius * 2);

                // Retrieve the shape object
                Shape shape = page.Shapes.GetShape((int)shapeId);

                // Set fill color based on index (cycle if more circles than colors)
                string fillColor = colors[i % colors.Length];
                shape.Fill.FillForegnd.Value = fillColor;
                shape.Fill.FillPattern.Value = 1; // Solid fill

                // Optional: set a thin black outline
                shape.Line.LineColor.Value = "#000000";
                shape.Line.LineWeight.Value = 0.02; // inches
                shape.Line.LinePattern.Value = LinePatternValue.Solid;
            }

            // Export the diagram as PNG
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
            diagram.Save("circles.png", saveOptions);
        }
    }