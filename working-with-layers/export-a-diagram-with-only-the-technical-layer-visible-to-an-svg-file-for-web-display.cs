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

            // Path to the source Visio diagram
            string inputPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages (handles multi‑page documents)
            foreach (Page page in diagram.Pages)
            {
                // Iterate through each layer on the page
                foreach (Layer layer in page.PageSheet.Layers)
                {
                    // Make only the "Technical" layer visible; hide all others
                    if (layer.Name.Value.Equals("Technical", StringComparison.OrdinalIgnoreCase))
                    {
                        layer.Visible.Value = BOOL.True;
                    }
                    else
                    {
                        layer.Visible.Value = BOOL.False;
                    }
                }
            }

            // Configure SVG export options
            SVGSaveOptions svgOptions = new SVGSaveOptions
            {
                ExportHiddenPage = false // exclude hidden pages from the export
            };

            // Path for the exported SVG file
            string outputPath = "output.svg";

            // Save the diagram as SVG using the configured options
            diagram.Save(outputPath, svgOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
