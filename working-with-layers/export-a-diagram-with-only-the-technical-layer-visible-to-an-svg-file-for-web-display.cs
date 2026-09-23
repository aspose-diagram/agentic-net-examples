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

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and set layer visibility
            foreach (Page page in diagram.Pages)
            {
                // Access the collection of layers on the page
                foreach (Layer layer in page.PageSheet.Layers)
                {
                    // Show only the layer named "Technical", hide others
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
            SVGSaveOptions svgOptions = new SVGSaveOptions();
            // Do not export hidden pages (optional, but ensures only visible content is saved)
            svgOptions.ExportHiddenPage = false;

            // Path to the output SVG file
            string outputPath = "output.svg";

            // Save the diagram as SVG with the specified options
            diagram.Save(outputPath, svgOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
