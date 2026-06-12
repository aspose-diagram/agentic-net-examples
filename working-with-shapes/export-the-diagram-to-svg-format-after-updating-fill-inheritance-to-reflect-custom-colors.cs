using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                try
                {
                    // Load the Visio diagram from a file.
                    // Replace "input.vsdx" with the actual path to your diagram.
                    Diagram diagram = new Diagram("input.vsdx");

                    // Iterate through all pages and shapes.
                    foreach (Page page in diagram.Pages)
                    {
                        foreach (Shape shape in page.Shapes)
                        {
                            // Skip deleted shapes.
                            if (shape.Del == BOOL.True)
                                continue;

                            // Update fill properties to reflect the inherited colors.
                            // This makes the fill explicit rather than inherited.
                            shape.Fill.FillForegnd.Value = shape.InheritFill.FillForegnd.Value;
                            shape.Fill.FillBkgnd.Value = shape.InheritFill.FillBkgnd.Value;
                            shape.Fill.FillPattern.Value = shape.InheritFill.FillPattern.Value;
                        }
                    }

                    // Configure SVG export options.
                    SVGSaveOptions svgOptions = new SVGSaveOptions
                    {
                        ExportHiddenPage = false,
                        ExportGuideShapes = false,
                        SVGFitToViewPort = true,
                        ExportElementAsRectTag = true
                    };

                    // Export the diagram to SVG.
                    // Replace "output.svg" with the desired output path.
                    diagram.Save("output.svg", svgOptions);
                }
                catch (Exception ex)
                {
                    // Simple error handling.
                    Console.WriteLine("An error occurred: " + ex.Message);
                    throw;
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }