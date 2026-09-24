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

                // Input Visio file path
                string inputPath = "input.vsdx";

                // Output folder for JPEG images
                string outputFolder = "ExportedShapes";
                if (!Directory.Exists(outputFolder))
                    Directory.CreateDirectory(outputFolder);

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Configure image save options with 600 DPI resolution
                ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Jpeg);
                imgOptions.Resolution = 600f;               // DPI
                imgOptions.ExportHiddenPage = false;        // Do not export hidden pages

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Build a unique file name for each shape
                        string shapeFileName = $"Page{page.ID}_Shape{shape.ID}.jpg";
                        string outputPath = Path.Combine(outputFolder, shapeFileName);

                        // Export the shape to JPEG using the configured options
                        shape.ToImage(outputPath, imgOptions);
                    }
                }

                Console.WriteLine("Shape export completed.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }