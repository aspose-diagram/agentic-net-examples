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

            // Load the diagram that contains the desired theme
            Diagram sourceDiagram = new Diagram("source.vsdx");

            // Load the diagram to which the theme will be applied
            Diagram targetDiagram = new Diagram("target.vsdx");

            // Apply the theme from the source diagram to the target diagram
            targetDiagram.CopyTheme(sourceDiagram);

            // Configure SVG save options
            SVGSaveOptions svgOptions = new SVGSaveOptions
            {
                // Fit the generated SVG to the viewport
                SVGFitToViewPort = true,
                // Export the first page (0‑based index); change as needed
                PageIndex = 0
            };

            // Save the themed diagram as SVG
            targetDiagram.Save("output.svg", svgOptions);

            // Clean up resources
            sourceDiagram.Dispose();
            targetDiagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
