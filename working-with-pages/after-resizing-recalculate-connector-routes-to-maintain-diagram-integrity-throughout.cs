using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Paths for input and output diagrams
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram from file
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Resize regular (non‑connector) shapes
                    if (!shape.OneD) // shape is not a connector
                    {
                        // Increase width and height by 10%
                        shape.XForm.Width.Value *= 1.1;
                        shape.XForm.Height.Value *= 1.1;
                    }
                    else // shape is a connector (1‑D)
                    {
                        // Recalculate routing: enforce right‑angle routing style
                        shape.Layout.ShapeRouteStyle.Value = ShapeRouteStyleValue.RightAngle;
                        // Reset reroute code to default (undefined)
                        shape.Layout.ConFixedCode.Value = ConFixedCodeValue.Undefined;
                        // Refresh connector geometry after changes
                        shape.RefreshData();
                    }
                }
            }

            // Save the updated diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
