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

            // Paths to the input Visio file and the output SVG file
            string inputPath = "input.vsdx";
            string outputPath = "output.svg";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate over all pages and shapes to modify connector routing
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Identify connector shapes (1‑D shapes)
                    if (shape.OneD)
                    {
                        // Set routing style to orthogonal (right‑angle)
                        shape.Layout.ShapeRouteStyle.Value = ShapeRouteStyleValue.RightAngle;
                    }
                }
            }

            // Configure SVG export options
            SVGSaveOptions svgOptions = new SVGSaveOptions
            {
                ExportHiddenPage = false
            };

            // Save the updated diagram as SVG
            diagram.Save(outputPath, svgOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
