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

            // Path to the source Visio file (VDX, VSDX, etc.)
            string inputPath = "input.vsdx";

            // Path where the HTML output will be saved
            string outputPath = "output.html";

            // Load the Visio diagram from the file system
            Diagram diagram = new Diagram(inputPath);

            // Configure HTML export options
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();

            // Do not export hidden pages (optional)
            htmlOptions.ExportHiddenPage = false;

            // Do not include comments in the HTML (optional)
            htmlOptions.IsExportComments = false;

            // Save the entire diagram as a single HTML file with embedded SVG shapes
            htmlOptions.SaveAsSingleFile = true;

            // Export the diagram to HTML using the configured options
            diagram.Save(outputPath, htmlOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
