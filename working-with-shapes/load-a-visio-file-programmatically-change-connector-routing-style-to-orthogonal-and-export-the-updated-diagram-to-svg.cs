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

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes to find connector shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Connectors are 1‑D shapes (dynamic connectors)
                    if (shape.OneD)
                    {
                        // Set routing style to orthogonal (right‑angle)
                        shape.Layout.ShapeRouteStyle.Value = ShapeRouteStyleValue.RightAngle;
                    }
                }
            }

            // Prepare SVG export options
            SVGSaveOptions svgOptions = new SVGSaveOptions();
            // Optional: do not export hidden pages
            svgOptions.ExportHiddenPage = false;

            // Export the updated diagram to SVG
            string outputPath = "output.svg";
            diagram.Save(outputPath, svgOptions);

            Console.WriteLine("Diagram exported to SVG successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
