using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class ExportShapesWithCustomDpi
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram from a file
            Diagram diagram = new Diagram("input.vsdx");

            // Ensure the output directory exists
            string outputFolder = "ExportedShapes";
            Directory.CreateDirectory(outputFolder);

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through each shape on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Configure image save options for JPEG with 600 DPI
                    ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Jpeg);
                    imgOptions.Resolution = 600;          // Set DPI
                    imgOptions.JpegQuality = 100;        // Optional: maximum quality

                    // Build a unique file name for the shape image
                    string imagePath = Path.Combine(
                        outputFolder,
                        $"Page_{page.ID}_Shape_{shape.ID}.jpg");

                    // Export the shape to a JPEG image using the specified options
                    shape.ToImage(imagePath, imgOptions);
                }
            }

            // Release resources
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
