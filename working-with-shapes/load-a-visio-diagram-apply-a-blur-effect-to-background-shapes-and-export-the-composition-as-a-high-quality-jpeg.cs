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

            // Input Visio file path
            string inputPath = "input.vsdx";

            // Output JPEG file path
            string outputPath = "output.jpg";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages to find background pages
            foreach (Page page in diagram.Pages)
            {
                // Check if the page is marked as a background page
                if (page.Background == BOOL.True)
                {
                    // Iterate through all shapes on the background page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Apply blur only to image (foreign) shapes that have an Image object
                        if (shape.Type == TypeValue.Foreign && shape.Image != null)
                        {
                            // Set blur intensity (value between 0.0 and 1.0)
                            shape.Image.Blur.Value = 0.25;
                        }
                    }
                }
            }

            // Configure high‑quality JPEG export options
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Jpeg);
            // Set a high resolution (e.g., 300 DPI) for better quality
            saveOptions.Resolution = 300f;
            // Ensure hidden pages are not exported (optional)
            saveOptions.ExportHiddenPage = false;

            // Save the modified diagram as a JPEG image
            diagram.Save(outputPath, saveOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
