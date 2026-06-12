using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class ExportShapesWithDpi
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string sourceFile = "input.vsdx";

            // Folder where JPEG images will be saved
            string outputFolder = "ExportedImages";

            // Load the Visio diagram
            Diagram diagram = new Diagram(sourceFile);

            // Ensure the output directory exists
            if (!Directory.Exists(outputFolder))
                Directory.CreateDirectory(outputFolder);

            // Iterate through all pages and their shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Build a unique file name for each shape
                    string imagePath = Path.Combine(
                        outputFolder,
                        $"Page{page.ID}_Shape{shape.ID}.jpg");

                    // Configure image save options with 600 DPI and JPEG format
                    ImageSaveOptions options = new ImageSaveOptions(SaveFileFormat.Jpeg);
                    options.Resolution = 600; // DPI setting

                    // Export the shape to JPEG using the specified options
                    shape.ToImage(imagePath, options);
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
