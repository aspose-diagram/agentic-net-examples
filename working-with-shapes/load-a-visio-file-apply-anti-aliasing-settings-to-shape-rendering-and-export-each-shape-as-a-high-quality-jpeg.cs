using System;
using System.IO;
using System.Drawing.Drawing2D;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class ExportShapesAsJpeg
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string visioFilePath = "input.vsdx";

            // Directory where individual shape images will be saved
            string outputFolder = "ExportedShapes";
            if (!Directory.Exists(outputFolder))
                Directory.CreateDirectory(outputFolder);

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
                        ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Jpeg);
                        imgOptions.SmoothingMode = SmoothingMode.AntiAlias; // enable anti‑aliasing
                        imgOptions.JpegQuality = 100;                       // maximum JPEG quality
                        imgOptions.Resolution = 300;                        // 300 DPI for high resolution

                        // Build a unique file name for the shape image
                        string shapeFileName = Path.Combine(
                            outputFolder,
                            $"Page_{page.ID}_Shape_{shape.ID}.jpg");

                        // Export the shape to a JPEG file using the specified options
                        shape.ToImage(shapeFileName, imgOptions);
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
