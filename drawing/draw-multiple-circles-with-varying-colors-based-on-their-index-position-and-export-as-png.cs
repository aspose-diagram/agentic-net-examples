using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define the output PNG file path
        string outputPath = "circles.png";

        try
        {
            // Create a new empty diagram (contains a default page)
            Diagram diagram = new Diagram();

            // Access the first (and only) page in the diagram
            Page page = diagram.Pages[0];

            // Parameters for drawing circles
            int circleCount = 5;          // total number of circles
            double startX = 2.0;          // initial X coordinate (in inches)
            double startY = 2.0;          // initial Y coordinate (in inches)
            double radius = 1.0;          // radius of each circle (in inches)
            double offset = 3.0;          // offset between successive circles (in inches)

            // Define a set of fill colors (hex strings)
            string[] colors = new string[]
            {
                "#FF0000", // Red
                "#00FF00", // Green
                "#0000FF", // Blue
                "#FFFF00", // Yellow
                "#FF00FF"  // Magenta
            };

            // Loop to create circles with varying colors
            for (int i = 0; i < circleCount; i++)
            {
                // Calculate the center position for the current circle
                double pinX = startX + i * offset;
                double pinY = startY + i * offset;

                // Draw an ellipse where width == height => circle
                // Parameters: pinX, pinY, width, height
                long shapeId = page.DrawEllipse(pinX, pinY, radius * 2, radius * 2);

                // Retrieve the shape object using the returned ID
                Shape shape = page.Shapes.GetShape(shapeId);

                // Ensure the shape has a solid fill pattern
                shape.Fill.FillPattern.Value = 1; // 1 = solid fill

                // Apply fill color based on index (cycle if more circles than colors)
                string fillColor = colors[i % colors.Length];
                shape.Fill.FillForegnd.Value = fillColor;
            }

            // Configure PNG export options
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
            saveOptions.PageIndex = 0;   // export the first page
            saveOptions.PageCount = 1;   // only one page to export

            // Save the diagram as a PNG image
            diagram.Save(outputPath, saveOptions);

            Console.WriteLine($"Diagram with circles saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}