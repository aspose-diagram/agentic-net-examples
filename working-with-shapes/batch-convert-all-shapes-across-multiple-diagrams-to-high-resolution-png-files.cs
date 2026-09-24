using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Input folder containing Visio files
            string inputFolder = @"C:\VisioFiles";
            // Output folder for PNG images
            string outputFolder = @"C:\VisioShapePngs";

            // Ensure output directory exists
            if (!Directory.Exists(outputFolder))
                Directory.CreateDirectory(outputFolder);

            // Get all Visio files (VSDX, VDX, VSD) in the input folder
            string[] visioFiles = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly);
            foreach (string filePath in visioFiles)
            {
                string extension = Path.GetExtension(filePath).ToLowerInvariant();
                if (extension != ".vsdx" && extension != ".vdx" && extension != ".vsd")
                    continue; // Skip non‑Visio files

                // Load the diagram
                Diagram diagram = new Diagram(filePath);

                // Iterate through each page
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through each shape on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Build a unique file name for the shape image
                        string shapeFileName = $"{Path.GetFileNameWithoutExtension(filePath)}_Page{page.ID}_Shape{shape.ID}.png";
                        string outputPath = Path.Combine(outputFolder, shapeFileName);

                        // Configure high‑resolution PNG export
                        ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);
                        pngOptions.Resolution = 300f; // 300 DPI

                        // Export the shape to PNG
                        shape.ToImage(outputPath, pngOptions);
                    }
                }
            }

            Console.WriteLine("Batch shape export completed.");
        }
    }