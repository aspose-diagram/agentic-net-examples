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
            string visioFilePath = @"C:\Input\diagram.vsdx";

            // Load the Visio diagram using the Diagram constructor (load rule)
            using (Diagram diagram = new Diagram(visioFilePath))
            {
                // Create a folder named with the current date (yyyyMMdd)
                string dateFolder = DateTime.Now.ToString("yyyyMMdd");
                string outputFolder = Path.Combine(@"C:\Output", dateFolder);
                Directory.CreateDirectory(outputFolder);

                // Prepare image save options for high‑resolution TIFF
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Tiff);
                saveOptions.Resolution = 300; // 300 DPI for high resolution

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Build a unique file name for each shape
                        string shapeFileName = $"Shape_{shape.ID}.tiff";
                        string shapeFilePath = Path.Combine(outputFolder, shapeFileName);

                        // Export the shape to a TIFF file using the ToImage method (shape conversion rule)
                        shape.ToImage(shapeFilePath, saveOptions);
                    }
                }
            }

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
