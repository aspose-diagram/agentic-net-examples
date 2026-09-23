using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define file paths.
        string sourceDiagramPath = "source.vsdx";
        string templateDiagramPath = "template.vsdx";
        string outputHtmlPath = "merged_output.html";

        // Verify source diagram exists.
        if (!File.Exists(sourceDiagramPath))
        {
            Console.Error.WriteLine($"File not found: {sourceDiagramPath}");
            return;
        }

        // Verify template diagram exists.
        if (!File.Exists(templateDiagramPath))
        {
            Console.Error.WriteLine($"File not found: {templateDiagramPath}");
            return;
        }

        try
        {
            // Load the source diagram.
            Diagram sourceDiagram = new Diagram(sourceDiagramPath);

            // Load the template diagram.
            Diagram templateDiagram = new Diagram(templateDiagramPath);

            // Merge the template into the source diagram.
            sourceDiagram.Combine(templateDiagram);

            // Configure HTML export options (embedded CSS is default behavior).
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions
            {
                ExportHiddenPage = false // Exclude hidden pages from the output.
            };

            // Save the merged diagram as HTML.
            sourceDiagram.Save(outputHtmlPath, htmlOptions);

            Console.WriteLine($"Diagram merged and saved to HTML at: {outputHtmlPath}");
        }
        catch (Exception ex)
        {
            // Output any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}