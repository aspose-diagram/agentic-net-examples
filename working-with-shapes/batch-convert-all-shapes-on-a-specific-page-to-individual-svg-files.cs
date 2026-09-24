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
                string visioPath = "input.vsdx";

                // Output folder for SVG files
                string outputFolder = "SvgOutput";

                // Ensure the output directory exists
                if (!Directory.Exists(outputFolder))
                    Directory.CreateDirectory(outputFolder);

                // Load the diagram
                using (Diagram diagram = new Diagram(visioPath))
                {
                    // Specify the page index to process (0‑based)
                    int pageIndex = 0;

                    // Retrieve the target page
                    Page page = diagram.Pages[pageIndex];

                    // Options for SVG export
                    SVGSaveOptions svgOptions = new SVGSaveOptions();

                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Build a unique file name for each shape
                        string svgFileName = $"Shape_{shape.ID}.svg";
                        string svgPath = Path.Combine(outputFolder, svgFileName);

                        // Export the shape to an individual SVG file
                        shape.ToSvg(svgPath, svgOptions);
                    }
                }

                Console.WriteLine("All shapes have been exported to SVG files.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }