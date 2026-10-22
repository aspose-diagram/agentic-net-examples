using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define output directory (current directory)
        string outputDir = Directory.GetCurrentDirectory();

        try
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Ensure the diagram has at least one page (Aspose.Diagram creates a default page)
            // We'll add additional pages to have a total of ten pages.
            for (int i = 1; i <= 10; i++)
            {
                // Add a new page to the diagram
                diagram.Pages.Add(new Page());

                // Retrieve the newly added page (pages are zero‑based)
                Page page = diagram.Pages[diagram.Pages.Count - 1];

                // Optional: give the page a name for clarity
                page.Name = $"Page_{i}";

                // Draw a diamond shape on the current page.
                // Using the built‑in "Diamond" master. Coordinates are in inches.
                // PinX, PinY represent the center of the shape.
                double pinX = 5.0;   // horizontal position
                double pinY = 5.0;   // vertical position
                double width = 2.0;  // width of the diamond
                double height = 2.0; // height of the diamond

                // Add the diamond shape to the diagram on the current page.
                // The AddShape method returns the shape ID (long).
                long shapeId = diagram.AddShape(pinX, pinY, width, height, "Diamond", diagram.Pages.Count - 1);

                // Retrieve the shape object if further manipulation is needed.
                // GetShape expects an int ID, so cast safely.
                Shape diamondShape = page.Shapes.GetShape((int)shapeId);
                // Example: set the shape's fill color to light gray (optional)
                // diamondShape.FillForegnd.Value = 0xD3D3D3; // LightGray RGB hex

                // Prepare PNG export options for the current page only.
                ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);
                pngOptions.PageIndex = diagram.Pages.Count - 1; // zero‑based page index
                pngOptions.PageCount = 1;                       // export only this page

                // Build the output file path.
                string outputPath = Path.Combine(outputDir, $"Diamond_Page_{i}.png");

                // Save the current page as a PNG image.
                diagram.Save(outputPath, pngOptions);

                Console.WriteLine($"Saved page {i} as PNG: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}