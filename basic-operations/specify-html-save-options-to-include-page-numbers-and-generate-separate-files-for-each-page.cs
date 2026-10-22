using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Output directory for HTML pages
        string outputDir = "HtmlPages";

        // Ensure output directory exists
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Set footer to display the page number using Visio placeholder code
            // The '&p' field inserts the current page number
            diagram.HeaderFooter.FooterRight = "Page: &p";

            // Iterate over each page and export it as a separate HTML file
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                // Configure HTML save options for a single page
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                {
                    PageIndex = i,   // zero‑based index of the page to export
                    PageCount = 1    // export only this page
                };

                // Build output file name (e.g., page_1.html, page_2.html, ...)
                string outputPath = Path.Combine(outputDir, $"page_{i + 1}.html");

                // Save the current page to HTML
                diagram.Save(outputPath, htmlOptions);
            }

            Console.WriteLine("HTML export completed successfully.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}