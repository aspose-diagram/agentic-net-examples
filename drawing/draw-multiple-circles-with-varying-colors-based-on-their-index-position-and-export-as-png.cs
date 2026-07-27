using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Use the first page (index 0)
            Page page = diagram.Pages[0];

            // Define circle parameters
            int circleCount = 5;
            double startX = 2.0; // inches
            double startY = 5.0; // inches
            double spacing = 2.5; // inches between circle centers
            double radius = 1.0; // inches (width and height will be 2*radius)

            // Define colors for each circle (hex strings)
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
                // Calculate center position for this circle
                double pinX = startX + i * spacing;
                double pinY = startY;

                // Draw an ellipse (circle) on the page
                // DrawEllipse expects: pinX, pinY, width, height
                long shapeId = page.DrawEllipse(pinX, pinY, radius * 2, radius * 2);

                // Retrieve the shape object (cast long to int for GetShape)
                Shape circleShape = page.Shapes.GetShape((int)shapeId);

                // Set the fill foreground color based on index
                string fillColor = colors[i % colors.Length];
                circleShape.Fill.FillForegnd.Value = fillColor;

                // Optional: remove outline by setting line pattern to 0 (no line)
                circleShape.Line.LinePattern.Value = 0;
            }

            // Prepare PNG export options
            ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);

            // Save the diagram as a PNG image
            string outputPath = "circles.png";
            diagram.Save(outputPath, pngOptions);

            Console.WriteLine($"Diagram with {circleCount} circles saved to '{outputPath}'.");
        }
    }