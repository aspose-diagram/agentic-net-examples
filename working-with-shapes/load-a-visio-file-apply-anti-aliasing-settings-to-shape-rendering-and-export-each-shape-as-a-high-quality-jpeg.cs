using System.IO;
using System;
using System.Drawing.Drawing2D;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class ExportShapesWithAntialiasing
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string visioFilePath = "input.vsdx";

            // Load the Visio diagram
            using (Diagram diagram = new Diagram(visioFilePath))
            {
                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through each shape on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Configure image save options for high‑quality JPEG with anti‑aliasing
                        ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Jpeg)
                        {
                            // Apply anti‑aliasing to lines, curves, and filled areas
                            SmoothingMode = SmoothingMode.AntiAlias,
                            // Set maximum JPEG quality
                            JpegQuality = 100,
                            // Use a high resolution for better detail
                            Resolution = 300
                        };

                        // Build a unique file name for each shape
                        string outputFileName = $"Shape_Page{page.ID}_Shape{shape.ID}.jpg";

                        // Export the shape as a JPEG image using the configured options
                        shape.ToImage(outputFileName, saveOptions);
                    }
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
