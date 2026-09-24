using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Prepare SVG save options with transparent background (default behavior)
            SVGSaveOptions svgOptions = new SVGSaveOptions
            {
                // Fit the SVG to the viewport; background remains transparent
                SVGFitToViewPort = true,
                // Do not export guide shapes
                ExportGuideShapes = false,
                // Do not export comments
                IsExportComments = false
            };

            // Export the first shape on the first page to SVG using the custom options
            Page firstPage = diagram.Pages[0];
            Shape firstShape = null;
            foreach (Shape shape in firstPage.Shapes)
            {
                firstShape = shape;
                break;
            }

            if (firstShape != null)
            {
                firstShape.ToSvg("shape_output.svg", svgOptions);
            }
            else
            {
                throw new Exception("No shapes found on the first page.");
            }

            // Optionally, export the entire diagram to SVG using the same options
            diagram.Save("diagram_output.svg", svgOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
