using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            // Create a new blank diagram (contains one default page)
            Diagram diagram = new Diagram();

            // Access the first page (avoid using ActivePage)
            Page page = diagram.Pages[0];

            // Define circle parameters
            int circleCount = 6;
            double radius = 1.0;               // inches
            double startX = 2.0;                // starting X position (center)
            double spacing = 3.0;               // horizontal spacing between circles
            double pinY = 5.0;                  // Y position (center)

            // Define a set of colors (hex strings) to apply based on index
            string[] colors = new string[]
            {
                "#FF0000", // Red
                "#00FF00", // Green
                "#0000FF", // Blue
                "#FFFF00", // Yellow
                "#FF00FF", // Magenta
                "#00FFFF"  // Cyan
            };

            // Draw circles and assign colors
            for (int i = 0; i < circleCount; i++)
            {
                double pinX = startX + i * spacing;

                // Draw an ellipse with equal width and height (a circle)
                long shapeId = page.DrawEllipse(pinX, pinY, radius * 2, radius * 2);

                // Retrieve the shape object to modify its properties
                Shape shape = page.Shapes.GetShape(shapeId);

                // Set a solid fill pattern
                shape.Fill.FillPattern.Value = 1; // 1 = solid

                // Assign fill color based on the index
                shape.Fill.FillForegnd.Value = colors[i % colors.Length];

                // Optional: set line color to black
                shape.Line.LineColor.Value = "#000000";
                shape.Line.LineWeight.Value = 0.02; // thin line
            }

            // Configure PNG export options
            ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);
            // Example: set resolution (dpi) if needed
            pngOptions.Resolution = 300f;

            // Save the diagram as a PNG image
            string outputPath = "Circles.png";
            diagram.Save(outputPath, pngOptions);

            Console.WriteLine($"Diagram with {circleCount} circles saved to '{outputPath}'.");
        }
    }