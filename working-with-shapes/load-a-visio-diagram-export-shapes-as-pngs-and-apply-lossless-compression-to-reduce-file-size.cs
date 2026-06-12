using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (change as needed)
                string inputPath = "input.vsdx";

                // Output directory for PNG files
                string outputDir = "ExportedShapes";
                Directory.CreateDirectory(outputDir);

                // Load the Visio diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Configure PNG export options (lossless by default)
                    ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);

                    // Iterate through all pages
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through all shapes on the page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Skip deleted shapes
                            if (shape.Del == BOOL.True)
                                continue;

                            // Build a safe file name using shape ID and optional name
                            string safeName = string.IsNullOrWhiteSpace(shape.NameU) ? "Shape" : shape.NameU;
                            // Replace any invalid filename characters
                            foreach (char c in Path.GetInvalidFileNameChars())
                                safeName = safeName.Replace(c, '_');

                            string outputPath = Path.Combine(
                                outputDir,
                                $"Page{page.ID}_Shape{shape.ID}_{safeName}.png");

                            // Export the shape to PNG
                            shape.ToImage(outputPath, pngOptions);
                        }
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