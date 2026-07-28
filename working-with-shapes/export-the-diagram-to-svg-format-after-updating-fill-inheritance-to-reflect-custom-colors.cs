using System;
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
                // Path for the exported SVG file
                string outputPath = "output.svg";

                // Load the diagram from file
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages and shapes to make fill inheritance explicit
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Copy inherited fill properties to the shape's own fill cells
                        // This makes custom colors permanent and not dependent on inheritance
                        shape.Fill.FillForegnd.Value = shape.InheritFill.FillForegnd.Value;
                        shape.Fill.FillBkgnd.Value = shape.InheritFill.FillBkgnd.Value;
                        shape.Fill.FillPattern.Value = shape.InheritFill.FillPattern.Value;
                    }
                }

                // Configure SVG export options
                SVGSaveOptions svgOptions = new SVGSaveOptions
                {
                    ExportHiddenPage = false,
                    SVGFitToViewPort = true
                };

                // Save the diagram as SVG using the options
                diagram.Save(outputPath, svgOptions);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }