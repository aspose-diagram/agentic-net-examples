using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using System.Drawing.Drawing2D;

class ExportShapes
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string visioPath = "input.vsdx";

            // Folder where the JPEG images will be saved
            string outputDir = "ShapeImages";
            Directory.CreateDirectory(outputDir);

            // Load the Visio diagram
            using (Diagram diagram = new Diagram(visioPath))
            {
                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through each shape on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Configure image save options for high‑quality JPEG with anti‑aliasing
                        ImageSaveOptions options = new ImageSaveOptions(SaveFileFormat.Jpeg);
                        options.SmoothingMode = SmoothingMode.AntiAlias; // enable anti‑aliasing
                        options.JpegQuality = 100;                       // maximum JPEG quality
                        options.Resolution = 300;                        // high DPI for better detail

                        // Create a unique file name for the shape image
                        string fileName = Path.Combine(
                            outputDir,
                            $"Page{page.ID}_Shape{shape.ID}.jpg");

                        // Export the shape as a JPEG image using the configured options
                        shape.ToImage(fileName, options);
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
