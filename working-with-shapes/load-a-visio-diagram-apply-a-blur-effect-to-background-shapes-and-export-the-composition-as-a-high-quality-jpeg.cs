using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Input Visio file (adjust the path as needed)
            string inputPath = "input.vsdx";

            // Output JPEG file (high‑quality)
            string outputPath = "output.jpg";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Apply a blur effect to all image shapes on background pages
            foreach (Page page in diagram.Pages)
            {
                // Identify background pages
                if (page.Background == BOOL.True)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Process only foreign (image) shapes that have an Image object
                        if (shape.Type == TypeValue.Foreign && shape.Image != null)
                        {
                            // Set blur amount (0.0 = no blur, 1.0 = maximum blur)
                            shape.Image.Blur.Value = 0.5; // Adjust as required
                        }
                    }
                }
            }

            // Configure high‑quality JPEG export
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Jpeg);
            saveOptions.Resolution = 300f;          // DPI for high quality
            saveOptions.PageIndex = 0;              // Export the first page (change if needed)
            saveOptions.ExportHiddenPage = false;   // Do not export hidden pages

            // Save the diagram as a JPEG image
            diagram.Save(outputPath, saveOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
