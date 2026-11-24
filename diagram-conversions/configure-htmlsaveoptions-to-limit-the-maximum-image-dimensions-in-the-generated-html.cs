using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (modify as needed)
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Configure HTML save options
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();

                // Do not export hidden pages
                htmlOptions.ExportHiddenPage = false; // BOOL.False is not assignable to bool

                // Do not export comments
                htmlOptions.IsExportComments = false; // BOOL.False is not assignable to bool

                // NOTE: HTMLSaveOptions does not provide a direct property to limit
                // the maximum image dimensions in the generated HTML. The closest
                // alternative is to control which pages are exported or to
                // post‑process the HTML files after export.

                // Save the diagram as HTML
                string outputPath = "output.html";
                diagram.Save(outputPath, htmlOptions);
                Console.WriteLine($"HTML export completed: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during processing: {ex.Message}");
        }
    }
}
