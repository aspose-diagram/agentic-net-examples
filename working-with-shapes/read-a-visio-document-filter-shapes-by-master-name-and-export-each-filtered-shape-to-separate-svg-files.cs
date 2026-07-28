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

                // Path to the source Visio file
                string inputPath = "input.vsdx";

                // Directory where individual SVG files will be saved
                string outputDirectory = "ExportedSvgs";

                // Master name to filter shapes (case‑sensitive)
                string targetMasterName = "Rectangle";

                // Ensure the output directory exists
                Directory.CreateDirectory(outputDirectory);

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Prepare SVG save options (default options are sufficient)
                SVGSaveOptions svgOptions = new SVGSaveOptions();

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Filter shapes that are based on the specified master
                        if (shape.Master != null && shape.Master.Name == targetMasterName)
                        {
                            // Build a unique file name for each shape
                            string safeShapeName = string.IsNullOrWhiteSpace(shape.Name) ? "Unnamed" : shape.Name.Replace(' ', '_');
                            string fileName = $"Shape_{shape.ID}_{safeShapeName}.svg";
                            string outputPath = Path.Combine(outputDirectory, fileName);

                            // Export the shape to SVG
                            shape.ToSvg(outputPath, svgOptions);

                            Console.WriteLine($"Exported shape ID {shape.ID} to {outputPath}");
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