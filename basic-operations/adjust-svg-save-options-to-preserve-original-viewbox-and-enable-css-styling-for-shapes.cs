using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Load an existing Visio diagram.
                // Replace "input.vsdx" with the path to your source file.
                Diagram diagram = new Diagram("input.vsdx");

                // Configure SVG save options.
                SVGSaveOptions svgOptions = new SVGSaveOptions();

                // Preserve the original viewbox by fitting the SVG to the viewport.
                // This ensures the exported SVG retains the same dimensions as the source diagram.
                svgOptions.SVGFitToViewPort = true;

                // Note: Aspose.Diagram does not provide a direct property to enable CSS styling
                // for shapes in the exported SVG. The library exports shape styles inline.
                // If CSS styling is required, post‑processing of the SVG file would be needed.

                // Export the diagram (first page) to SVG using the configured options.
                // Adjust the output path as needed.
                diagram.Save("output.svg", svgOptions);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }