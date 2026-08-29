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

            // Path for the exported SVG file
            string outputPath = "output.svg";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate over all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate over all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Connectors are 1‑D shapes
                    if (shape.OneD)
                    {
                        // Set connector routing style to orthogonal (right‑angle)
                        shape.Layout.ShapeRouteStyle.Value = ShapeRouteStyleValue.RightAngle;
                    }
                }
            }

            // Export the updated diagram to SVG
            diagram.Save(outputPath, new SVGSaveOptions());

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
