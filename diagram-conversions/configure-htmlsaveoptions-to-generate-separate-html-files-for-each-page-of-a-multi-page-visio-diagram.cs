using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found at '{inputPath}'.");
            return;
        }

        Diagram diagram = null;
        try
        {
            // Load the Visio diagram
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
            return;
        }

        try
        {
            // Verify the diagram contains at least one page
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages to export.");
                return;
            }

            // Export each page as a separate HTML file
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                // Configure HTML save options for a single page
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                {
                    PageIndex = i,          // Zero‑based index of the page to export
                    PageCount = 1,          // Export only one page
                    ExportHiddenPage = false // Do not export hidden pages
                };

                // Build output file name (e.g., page_1.html, page_2.html, ...)
                string outputPath = $"page_{i + 1}.html";

                try
                {
                    // Save the current page to HTML
                    diagram.Save(outputPath, htmlOptions);
                    Console.WriteLine($"Page {i + 1} saved to '{outputPath}'.");
                }
                catch (Exception saveEx)
                {
                    Console.Error.WriteLine($"Error saving page {i + 1}: {saveEx.Message}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }

        // No explicit disposal needed; Diagram does not implement IDisposable
    }
}