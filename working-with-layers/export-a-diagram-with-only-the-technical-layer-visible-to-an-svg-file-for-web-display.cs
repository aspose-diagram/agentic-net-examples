using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Input Visio file path
            string inputPath = "input.vsdx";
            // Output SVG file path
            string outputPath = "output.svg";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and set layer visibility
            foreach (Page page in diagram.Pages)
            {
                foreach (Layer layer in page.PageSheet.Layers)
                {
                    // Make only the 'Technical' layer visible
                    if (layer.Name.Value == "Technical")
                        layer.Visible.Value = BOOL.True;
                    else
                        layer.Visible.Value = BOOL.False;
                }
            }

            // Configure SVG save options (optional: exclude hidden pages)
            SVGSaveOptions svgOptions = new SVGSaveOptions();
            svgOptions.ExportHiddenPage = false;

            // Save the diagram as SVG
            diagram.Save(outputPath, svgOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
