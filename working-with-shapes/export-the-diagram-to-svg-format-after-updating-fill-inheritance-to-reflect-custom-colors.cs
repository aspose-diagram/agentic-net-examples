using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";
                // Path for the exported SVG file
                string outputPath = "output.svg";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // If the shape's fill foreground color is inherited, make it explicit
                        if (shape.Fill.FillForegnd.Value == shape.InheritFill.FillForegnd.Value)
                        {
                            // Copy the inherited foreground color to the shape's own fill cell
                            shape.Fill.FillForegnd.Value = shape.InheritFill.FillForegnd.Value;
                        }

                        // If the shape's fill background color is inherited, make it explicit
                        if (shape.Fill.FillBkgnd.Value == shape.InheritFill.FillBkgnd.Value)
                        {
                            shape.Fill.FillBkgnd.Value = shape.InheritFill.FillBkgnd.Value;
                        }

                        // If the shape's fill pattern is inherited, make it explicit
                        if (shape.Fill.FillPattern.Value == shape.InheritFill.FillPattern.Value)
                        {
                            shape.Fill.FillPattern.Value = shape.InheritFill.FillPattern.Value;
                        }
                    }
                }

                // Configure SVG export options
                SVGSaveOptions svgOptions = new SVGSaveOptions
                {
                    ExportHiddenPage = false,
                    ExportGuideShapes = false,
                    SVGFitToViewPort = true,
                    ExportElementAsRectTag = true
                };

                // Save the diagram as SVG
                diagram.Save(outputPath, svgOptions);

                Console.WriteLine($"Diagram exported to SVG successfully: {outputPath}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }