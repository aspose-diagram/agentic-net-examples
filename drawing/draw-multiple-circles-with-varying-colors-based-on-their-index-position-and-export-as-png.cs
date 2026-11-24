using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (must exist). Provide via first argument or use default.
        string inputPath = args.Length > 0 ? args[0] : "template.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Output PNG file path. Provide via second argument or use default.
        string outputPath = args.Length > 1 ? args[1] : "circles.png";

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure there is at least one page to draw on.
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Work with the first page.
                Page page = diagram.Pages[0];

                // Define colors for circles (hex strings).
                string[] colors = new string[]
                {
                    "#FF0000", // Red
                    "#00FF00", // Green
                    "#0000FF", // Blue
                    "#FFFF00", // Yellow
                    "#FF00FF", // Magenta
                    "#00FFFF", // Cyan
                    "#FFA500", // Orange
                    "#800080"  // Purple
                };

                // Number of circles to draw.
                int circleCount = 8;

                // Parameters for positioning.
                double startX = 2.0;   // Starting X coordinate.
                double startY = 5.0;   // Y coordinate (same for all circles).
                double spacing = 3.0;  // Horizontal spacing between circles.
                double radius = 1.0;   // Radius of each circle.

                for (int i = 0; i < circleCount; i++)
                {
                    // Calculate center position for the current circle.
                    double pinX = startX + i * spacing;
                    double pinY = startY;
                    double diameter = radius * 2;

                    // Draw an ellipse (circle) on the page.
                    long shapeId = page.DrawEllipse(pinX, pinY, diameter, diameter);

                    // Retrieve the created shape using its ID.
                    Shape shape = page.Shapes.GetShape(shapeId);

                    // Apply fill background color based on the index.
                    string colorHex = colors[i % colors.Length];
                    shape.Fill.FillBkgnd.Value = colorHex;

                    // Ensure the shape has a visible outline (optional).
                    shape.Line.LineColor.Value = "#000000";
                }

                // Prepare PNG export options.
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
                // Export only the first page.
                saveOptions.PageIndex = 0;
                saveOptions.PageCount = 1;

                // Save the diagram as a PNG image.
                diagram.Save(outputPath, saveOptions);
                Console.WriteLine($"Diagram saved as PNG to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}