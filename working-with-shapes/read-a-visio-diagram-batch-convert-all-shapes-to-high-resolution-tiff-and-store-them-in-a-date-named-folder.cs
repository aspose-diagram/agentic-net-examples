using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class VisioShapeToTiffBatch
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string sourceVisioPath = @"C:\VisioFiles\sample.vsdx";

            // Load the Visio diagram using the Diagram constructor (load rule)
            Diagram diagram = new Diagram(sourceVisioPath);

            // Create a folder named with the current date (yyyyMMdd) inside an "Output" directory
            string dateFolderName = DateTime.Now.ToString("yyyyMMdd");
            string outputRoot = Path.Combine(@"C:\VisioFiles\Output", dateFolderName);
            Directory.CreateDirectory(outputRoot);

            // Iterate through all pages and shapes
            int pageIndex = 0;
            foreach (Page page in diagram.Pages)
            {
                int shapeIndex = 0;
                foreach (Shape shape in page.Shapes)
                {
                    // Build a unique file name for each shape
                    string tiffFilePath = Path.Combine(
                        outputRoot,
                        $"Page{pageIndex}_Shape{shapeIndex}.tiff");

                    // Configure high‑resolution image options (300 DPI)
                    ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Tiff);
                    saveOptions.Resolution = 300; // high‑resolution DPI

                    // Export the shape to TIFF using the ToImage method (save rule)
                    shape.ToImage(tiffFilePath, saveOptions);

                    shapeIndex++;
                }
                pageIndex++;
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
