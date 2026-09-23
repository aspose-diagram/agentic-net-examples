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

            // Input Visio file path
            string inputPath = "input.vsdx";

            // Output SVG file path
            string outputPath = "output.svg";

            // Load the diagram from file
            Diagram diagram = new Diagram(inputPath);

            // Set up SVG save options to include hidden pages
            SVGSaveOptions svgOptions = new SVGSaveOptions();
            svgOptions.ExportHiddenPage = true;

            // Export the diagram to SVG using the configured options
            diagram.Save(outputPath, svgOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
