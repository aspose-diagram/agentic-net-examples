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
            Diagram diagram = new Diagram("input.vsdx");

            // Set up HTML save options to embed all resources (including SVG) in a single file
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions
            {
                SaveAsSingleFile = true,          // embed images/SVG inline
                ExportHiddenPage = false,         // do not export hidden pages
                ExportGuideShapes = false,        // skip guide shapes
                IsExportComments = false,         // skip comments
                SaveToolBar = false               // optional: omit toolbar for cleaner output
            };

            // Export the diagram to HTML with inline SVG markup
            diagram.Save("output.html", htmlOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
