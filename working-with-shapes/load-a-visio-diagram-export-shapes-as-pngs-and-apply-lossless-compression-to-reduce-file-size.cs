using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class ExportShapesAsPng
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string sourceFile = "input.vsdx";

            // Load the Visio diagram using the constructor that accepts a file path
            Diagram diagram = new Diagram(sourceFile);

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through each shape on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Build a unique file name for the exported PNG
                    string pngFile = $"shape_page{page.ID}_shape{shape.ID}.png";

                    // Create image save options for PNG format (lossless by nature)
                    ImageSaveOptions options = new ImageSaveOptions(SaveFileFormat.Png);

                    // Export the shape to a PNG file using the ToImage method
                    shape.ToImage(pngFile, options);
                }
            }

            // Release resources held by the diagram
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
