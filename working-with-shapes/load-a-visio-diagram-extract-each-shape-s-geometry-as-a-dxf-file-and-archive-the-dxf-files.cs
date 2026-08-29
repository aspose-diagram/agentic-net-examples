using System;
using System.IO;
using System.IO.Compression;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (adjust as needed)
                string visioPath = "input.vsdx";

                // Temporary folder to store individual SVG files (used as a stand‑in for DXF)
                string tempFolder = Path.Combine(Path.GetTempPath(), "VisioShapeExports");
                Directory.CreateDirectory(tempFolder);

                // Output ZIP archive path
                string zipPath = "ShapeGeometries.zip";

                // Load the Visio diagram
                Diagram diagram = new Diagram(visioPath);

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Build a file name for the shape export
                        string shapeFileName = $"Shape_{shape.ID}.svg";
                        string shapeFilePath = Path.Combine(tempFolder, shapeFileName);

                        // Export the shape geometry to SVG (DXF not supported by Aspose.Diagram)
                        SVGSaveOptions svgOptions = new SVGSaveOptions();
                        shape.ToSvg(shapeFilePath, svgOptions);
                    }
                }

                // Create a ZIP archive containing all exported files
                if (File.Exists(zipPath))
                    File.Delete(zipPath);

                ZipFile.CreateFromDirectory(tempFolder, zipPath);

                // Clean up temporary files
                Directory.Delete(tempFolder, true);

                Console.WriteLine($"Export completed. Archive created at: {Path.GetFullPath(zipPath)}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }