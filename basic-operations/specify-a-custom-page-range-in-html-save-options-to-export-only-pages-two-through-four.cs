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
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output directory for HTML pages
        string outputDir = "HtmlPages";

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Ensure output directory exists
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Determine total page count
            int pageCount = diagram.Pages.Count;

            // Export pages 2 through 4 (1‑based page numbers)
            // Corresponding zero‑based indices are 1, 2, 3
            for (int i = 1; i <= 3 && i < pageCount; i++)
            {
                // Configure HTML save options for a single page
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                {
                    // Export only the current page
                    PageIndex = i,
                    PageCount = 1
                };

                // Build output file name (e.g., page_2.html)
                string outputPath = Path.Combine(outputDir, $"page_{i + 1}.html");

                // Save the specific page as HTML
                diagram.Save(outputPath, htmlOptions);
                Console.WriteLine($"Exported page {i + 1} to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}