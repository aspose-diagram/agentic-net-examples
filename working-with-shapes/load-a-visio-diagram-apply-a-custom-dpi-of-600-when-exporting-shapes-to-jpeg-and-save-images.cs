using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class ExportShapesWithCustomDpi
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram from a file
            var diagram = new Diagram("input.vsdx");

            // Prepare image save options for JPEG with 600 DPI
            var imgOptions = new ImageSaveOptions(SaveFileFormat.Jpeg)
            {
                Resolution = 600,          // Set custom DPI
                SaveFormat = SaveFileFormat.Jpeg
            };

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Build a unique file name for each shape
                    string fileName = $"Shape_Page{page.ID}_Shape{shape.ID}.jpg";

                    // Export the shape to JPEG using the specified options
                    shape.ToImage(fileName, imgOptions);
                }
            }

            // Dispose the diagram when done
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
