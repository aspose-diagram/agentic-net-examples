using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path.
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output HTML file path.
        string outputPath = "output.html";

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Configure HTML save options to export pages 2 through 4.
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions
            {
                // PageIndex is zero‑based; 1 corresponds to the second page.
                PageIndex = 1,
                // Export three pages: pages 2, 3, and 4.
                PageCount = 3,
                // Do not export hidden pages.
                ExportHiddenPage = false
            };

            // Save the diagram as HTML using the configured options.
            diagram.Save(outputPath, htmlOptions);
            Console.WriteLine($"HTML export completed: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
