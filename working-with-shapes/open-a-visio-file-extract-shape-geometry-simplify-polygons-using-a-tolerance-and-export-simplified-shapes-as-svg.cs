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

                // Output directory for SVG files
                string outputDir = "output_svgs";

                // Ensure the output directory exists
                Directory.CreateDirectory(outputDir);

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Tolerance for polygon simplification (placeholder value)
                double tolerance = 0.01;

                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through each shape on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted
                        if (shape.Del == BOOL.True)
                            continue;

                        // ----- Geometry Simplification Placeholder -----
                        // Here you would extract the shape's geometry (shape.Geoms),
                        // apply a polygon simplification algorithm such as Douglas-Peucker
                        // using the specified tolerance, and rebuild the geometry.
                        // For brevity, this example directly exports the original shape.

                        // Generate a unique SVG file name for the shape
                        string svgPath = Path.Combine(outputDir, $"shape_{shape.ID}.svg");

                        // Export the shape to SVG
                        SVGSaveOptions svgOptions = new SVGSaveOptions();
                        shape.ToSvg(svgPath, svgOptions);
                    }
                }

                // Optionally, save the diagram back if any modifications were made
                // diagram.Save("modified_output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }