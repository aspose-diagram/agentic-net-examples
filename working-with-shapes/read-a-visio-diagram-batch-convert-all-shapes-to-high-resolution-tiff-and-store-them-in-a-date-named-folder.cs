using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string visioPath = @"C:\Input\diagram.vsdx";

            // Load the Visio diagram using the Diagram constructor (load rule)
            using (Diagram diagram = new Diagram(visioPath))
            {
                // Create an output folder named with the current date (yyyyMMdd)
                string dateFolder = DateTime.Now.ToString("yyyyMMdd");
                string outputDir = Path.Combine(@"C:\Output", dateFolder);
                Directory.CreateDirectory(outputDir);

                // Prepare image save options for high‑resolution TIFF
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Tiff)
                {
                    // Set a high resolution (e.g., 300 DPI). Adjust as needed.
                    Resolution = 300
                };

                // Iterate through all pages and their shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Build a unique file name for each shape
                        string fileName = $"Shape_{shape.ID}.tiff";
                        string filePath = Path.Combine(outputDir, fileName);

                        // Export the shape to a TIFF image using the ToImage method (rule)
                        shape.ToImage(filePath, saveOptions);
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
