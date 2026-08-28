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

            // Load the Visio diagram
            Diagram diagram = new Diagram("input.vsd");

            // Assign a CSS class (via the shape name) to each shape on the first page
            int shapeIndex = 0;
            foreach (Shape shape in diagram.Pages[0].Shapes)
            {
                // Example class name: "shape-0", "shape-1", ...
                shape.Name = $"shape-{shapeIndex}";
                shapeIndex++;
            }

            // Configure SVG save options
            SVGSaveOptions svgOptions = new SVGSaveOptions
            {
                // Make the generated SVG fit the viewport
                SVGFitToViewPort = true
            };

            // Export the entire diagram to a single SVG file
            diagram.Save("output.svg", svgOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
