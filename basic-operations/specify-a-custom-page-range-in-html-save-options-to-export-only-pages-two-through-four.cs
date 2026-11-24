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

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Output directory for HTML files
        string outputDir = "HtmlPages";

        // Guard: create output directory if it does not exist
        try
        {
            if (!Directory.Exists(outputDir))
                Directory.CreateDirectory(outputDir);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to create output directory: {ex.Message}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Determine total page count
            int totalPages = diagram.Pages.Count;

            // Define the custom page range (pages 2 through 4, 1‑based indexing)
            int startPage = 2; // inclusive
            int endPage = 4;   // inclusive

            // Validate the requested range
            if (startPage < 1 || endPage > totalPages || startPage > endPage)
            {
                Console.Error.WriteLine("Invalid page range specified.");
                return;
            }

            // Loop through the specified pages and export each as a separate HTML file
            for (int i = startPage - 1; i < endPage; i++) // zero‑based index
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

                // Save the current page to HTML
                diagram.Save(outputPath, htmlOptions);

                Console.WriteLine($"Exported page {i + 1} to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during processing: {ex.Message}");
        }
    }
}