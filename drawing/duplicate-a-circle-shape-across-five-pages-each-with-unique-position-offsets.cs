using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define the output file path
        string outputPath = "output.vsdx";

        // Guard: ensure the output directory exists
        string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        try
        {
            // Create a new empty diagram (contains a default page)
            Diagram diagram = new Diagram();

            // Add additional pages so we have a total of five pages
            // The diagram already has page index 0, so add pages 1..4
            for (int i = 1; i < 5; i++)
            {
                // Create a new blank page and add it to the diagram
                Page newPage = new Page();
                diagram.Pages.Add(newPage);
            }

            // Loop through each of the five pages and draw a circle with an offset
            for (int i = 0; i < 5; i++)
            {
                // Retrieve the current page by index
                Page page = diagram.Pages[i];

                // Calculate unique position offsets for this page
                double offsetX = i * 2.0; // 2 inches offset per page on X axis
                double offsetY = i * 1.0; // 1 inch offset per page on Y axis

                // Define the center of the circle (PinX, PinY) with offsets
                double centerX = 5.0 + offsetX;
                double centerY = 5.0 + offsetY;

                // Define the diameter of the circle (width = height)
                double diameter = 1.0; // 1 inch diameter

                // Draw the circle (ellipse with equal width and height) on the page
                long shapeId = page.DrawEllipse(centerX, centerY, diameter, diameter);

                // Retrieve the newly created shape object
                Shape shape = page.Shapes.GetShape((int)shapeId);

                // Set a label for the shape to identify the page
                shape.Text.Value.Clear();
                shape.Text.Value.Add(new Txt($"Circle {i + 1}"));

                // Apply a simple fill color for visual distinction
                shape.Fill.FillForegnd.Value = "#FFCCCC"; // light red fill
                shape.Fill.FillPattern.Value = 1; // solid fill pattern
            }

            // Save the diagram in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}