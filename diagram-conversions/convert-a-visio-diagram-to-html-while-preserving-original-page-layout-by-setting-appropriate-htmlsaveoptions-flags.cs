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
            string sourcePath = "input.vsdx";

            // Path for the generated HTML output
            string outputPath = "output.html";

            // Load the Visio diagram
            Diagram diagram = new Diagram(sourcePath);

            // Configure HTML export options to keep the original page layout
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions
            {
                // Do not export hidden pages (preserves visible layout only)
                ExportHiddenPage = false,
                // Do not include comments in the HTML output
                IsExportComments = false
            };

            // Save the diagram as HTML using the configured options
            diagram.Save(outputPath, htmlOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
