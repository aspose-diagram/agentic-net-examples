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
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Example geometry update: add a new line segment to the first shape on the first page
            if (diagram.Pages.Count > 0)
            {
                Page page = diagram.Pages[0];
                if (page.Shapes.Count > 0)
                {
                    // Retrieve the first shape (cast long ID to int if needed)
                    Shape shape = page.Shapes.GetShape(page.Shapes[0].ID);

                    // Ensure the shape has at least one geometry section
                    if (shape.Geoms.Count > 0)
                    {
                        // Get the first geometry
                        Geom geom = (Geom)shape.Geoms[0];

                        // Add a MoveTo at the current position (optional, ensures a start point)
                        MoveTo move = new MoveTo();
                        move.X.Value = shape.XForm.PinX.Value;
                        move.Y.Value = shape.XForm.PinY.Value;
                        geom.CoordinateCol.Add(move);

                        // Append a new line segment (LineTo) to extend the shape
                        LineTo line = new LineTo();
                        line.X.Value = shape.XForm.PinX.Value + 1.0; // extend 1 inch to the right
                        line.Y.Value = shape.XForm.PinY.Value;      // same vertical position
                        geom.CoordinateCol.Add(line);
                    }
                }
            }

            // Configure SVG export options
            SVGSaveOptions svgOptions = new SVGSaveOptions();
            svgOptions.ExportHiddenPage = false; // do not export hidden pages
            svgOptions.ExportGuideShapes = false; // optional: exclude guide shapes
            svgOptions.SVGFitToViewPort = true;   // fit SVG to viewport

            // Export the updated diagram to SVG
            string outputPath = "output.svg";
            diagram.Save(outputPath, svgOptions);

            Console.WriteLine($"Diagram exported to SVG at: {outputPath}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
