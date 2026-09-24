using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path – change as needed.
        string inputPath = "input.vsdx";
        // Guard to ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output SVG file path.
        string outputPath = "output.svg";

        try
        {
            // Load the diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // If the shape's foreground fill color is empty, copy the inherited value.
                    if (string.IsNullOrWhiteSpace(shape.Fill.FillForegnd.Value))
                    {
                        shape.Fill.FillForegnd.Value = shape.InheritFill.FillForegnd.Value;
                    }

                    // If the shape's background fill color is empty, copy the inherited value.
                    if (string.IsNullOrWhiteSpace(shape.Fill.FillBkgnd.Value))
                    {
                        shape.Fill.FillBkgnd.Value = shape.InheritFill.FillBkgnd.Value;
                    }

                    // If the shape's fill pattern is zero (default), copy the inherited pattern.
                    if (shape.Fill.FillPattern.Value == 0)
                    {
                        shape.Fill.FillPattern.Value = shape.InheritFill.FillPattern.Value;
                    }
                }
            }

            // Configure SVG save options – hide hidden pages and export guide shapes.
            SVGSaveOptions svgOptions = new SVGSaveOptions
            {
                ExportHiddenPage = false,
                ExportGuideShapes = false,
                SVGFitToViewPort = true
            };

            // Export the updated diagram to SVG using the configured options.
            diagram.Save(outputPath, svgOptions);

            Console.WriteLine($"Diagram successfully exported to SVG: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}