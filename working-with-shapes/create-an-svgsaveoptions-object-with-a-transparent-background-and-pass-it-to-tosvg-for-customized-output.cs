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

            // Load an existing Visio diagram.
            // Replace the path with the actual file location.
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Get the first page of the diagram.
            Page page = diagram.Pages[0];

            // Retrieve the first shape on the page.
            Shape shape = null;
            foreach (Shape s in page.Shapes)
            {
                shape = s;
                break;
            }

            if (shape == null)
            {
                throw new Exception("No shape found on the first page.");
            }

            // Create SVG save options.
            // By default the background is transparent; no explicit property exists to set it.
            SVGSaveOptions svgOptions = new SVGSaveOptions();

            // Optional customizations:
            svgOptions.EnlargePage = false;          // Do not enlarge the page to fit content.
            svgOptions.SVGFitToViewPort = true;      // Fit the SVG to the viewport.
            svgOptions.IsExportComments = false;     // Do not export comments.

            // Export the shape to an SVG file using the customized options.
            string outputPath = "shape_output.svg";
            shape.ToSvg(outputPath, svgOptions);

            Console.WriteLine($"Shape exported to SVG at: {outputPath}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
