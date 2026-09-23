using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Path to the source Visio file (multi‑page diagram)
        string sourcePath = "input.vsdx";

        // Verify the source file exists before proceeding
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"File not found: {sourcePath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(sourcePath);

            // Determine the total number of pages in the diagram
            int totalPages = diagram.Pages.Count;

            // Iterate over each page and export it as an individual HTML file
            for (int i = 0; i < totalPages; i++)
            {
                // Configure HTML save options for a single page export
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                {
                    // Export only the current page (zero‑based index)
                    PageIndex = i,
                    // Limit export to one page
                    PageCount = 1,
                    // Exclude hidden pages from the output
                    ExportHiddenPage = false
                };

                // Build a distinct output file name for each page
                string outputPath = $"output_page_{i + 1}.html";

                // Save the current page as HTML using the configured options
                diagram.Save(outputPath, htmlOptions);
            }

            Console.WriteLine("HTML export completed. Separate files generated for each page.");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error during HTML export: {ex.Message}");
        }
    }
}