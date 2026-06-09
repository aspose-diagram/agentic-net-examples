using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Manipulation;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Process only connector shapes (1‑D shapes)
                    if (shape.OneD)
                    {
                        // Set connector routing style to RightAngle for clearer routing
                        shape.Layout.ShapeRouteStyle.Value = ShapeRouteStyleValue.RightAngle;

                        // Optionally enforce a square jump style at intersections
                        shape.Layout.ConLineJumpStyle.Value = ConLineJumpStyleValue.Square;

                        // Ensure jumps are always applied
                        shape.Layout.ConLineJumpCode.Value = ConLineJumpCodeValue.Always;
                    }
                }
            }

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
