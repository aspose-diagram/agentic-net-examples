using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Get the first page (use Pages collection, not ActivePage)
            Page page = diagram.Pages[0];

            // Define circle parameters
            int circleCount = 5;
            double startX = 2.0;      // starting X position (in inches)
            double startY = 5.0;      // Y position for all circles
            double spacing = 3.0;     // horizontal spacing between circles
            double radius = 1.0;      // radius of each circle (in inches)

            // Define a set of colors (hex strings) to use based on index
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
                // Calculate center position for the current circle
                double pinX = startX + i * spacing;
                double pinY = startY;

                // Width and height are diameters (2 * radius)
                double diameter = radius * 2.0;

                // Draw the circle (ellipse with equal width and height)
                long shapeId = page.DrawEllipse(pinX, pinY, diameter, diameter);

                // Retrieve the shape object (GetShape expects an int)
                Shape shape = page.Shapes.GetShape((int)shapeId);

                // Apply solid fill pattern
                shape.Fill.FillPattern.Value = 1; // 1 = solid

                // Set fill color based on index (cycle if more circles than colors)
                string fillColor = colors[i % colors.Length];
                shape.Fill.FillForegnd.Value = fillColor;

                // Optional: remove outline by setting line pattern to 0 (no line)
                shape.Line.LinePattern.Value = 0;
            }

            // Configure PNG export options
            ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);

            // Save the diagram as a PNG image
            diagram.Save("circles.png", pngOptions);
        }
    }