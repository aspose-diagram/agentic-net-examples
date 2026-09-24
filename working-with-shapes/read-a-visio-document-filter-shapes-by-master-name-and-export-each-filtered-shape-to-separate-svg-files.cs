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

                // Input Visio file path
                string inputPath = "input.vsdx";

                // Directory where individual SVG files will be saved
                string outputDir = "ExportedSvgs";

                // Master name to filter shapes (adjust as needed)
                string targetMasterName = "Rectangle";

                // Ensure the output directory exists
                if (!Directory.Exists(outputDir))
                    Directory.CreateDirectory(outputDir);

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes and ensure the shape has a master
                        if (shape.Del == BOOL.False && shape.Master != null && shape.Master.Name == targetMasterName)
                        {
                            // Build a unique file name for each shape
                            string fileName = $"Shape_{shape.ID}_{Guid.NewGuid()}.svg";
                            string outputPath = Path.Combine(outputDir, fileName);

                            // Export the shape to SVG
                            SVGSaveOptions svgOptions = new SVGSaveOptions();
                            shape.ToSvg(outputPath, svgOptions);
                        }
                    }
                }

                Console.WriteLine("Export completed.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }