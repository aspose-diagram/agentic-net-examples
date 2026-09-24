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

                // Path to the source Visio file (modify as needed)
                string inputPath = "input.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Create a folder named with the current date (yyyyMMdd) in the current directory
                string dateFolder = DateTime.Now.ToString("yyyyMMdd");
                string outputDir = Path.Combine(Directory.GetCurrentDirectory(), dateFolder);
                Directory.CreateDirectory(outputDir);

                // Iterate through each page and each shape on the page
                int pageNumber = 0;
                foreach (Page page in diagram.Pages)
                {
                    pageNumber++;

                    foreach (Aspose.Diagram.Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted
                        if (shape.Del == BOOL.True)
                            continue;

                        // Build a unique file name for each shape
                        string fileName = $"Page{pageNumber}_Shape{shape.ID}.tiff";
                        string outputPath = Path.Combine(outputDir, fileName);

                        // Configure high‑resolution TIFF export options
                        ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Tiff);
                        saveOptions.Resolution = 300f; // DPI

                        // Export the shape to a TIFF file
                        shape.ToImage(outputPath, saveOptions);
                    }
                }

                Console.WriteLine($"All shapes have been exported to: {outputDir}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }