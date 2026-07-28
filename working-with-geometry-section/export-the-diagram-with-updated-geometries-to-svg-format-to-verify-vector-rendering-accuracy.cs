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

            // Load the Visio diagram from a file
            Diagram diagram = new Diagram("input.vsd");

            // -------------------------------------------------
            // Here you can modify shape geometries if needed.
            // For example, iterate through shapes and adjust
            // their XForm geometry properties.
            // -------------------------------------------------
            // foreach (Shape shape in diagram.Pages[0].Shapes)
            // {
            //     // shape.XForm.PinX = ...;
            //     // shape.XForm.PinY = ...;
            // }

            // Configure SVG save options
            SVGSaveOptions svgOptions = new SVGSaveOptions
            {
                // Export rectangle shapes as <rect> tags (optional)
                ExportElementAsRectTag = true,
                // Keep scale information in the transformation matrix
                IsExportScaleInMatrix = true,
                // Fit the generated SVG to the viewport
                SVGFitToViewPort = true
            };

            // Save the entire diagram as an SVG file using the options above
            diagram.Save("output.svg", svgOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
