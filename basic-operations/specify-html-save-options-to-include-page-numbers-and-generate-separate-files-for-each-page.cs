using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Determine input Visio file path (argument or default)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (mandatory pattern)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Set a global footer that includes the page number using Visio's &p field code
                diagram.HeaderFooter.FooterRight = "Page: &p";

                // Iterate over each page to export it as a separate HTML file
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    // Configure HTML save options to export only the current page
                    HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                    {
                        PageIndex = i,   // zero‑based index of the page to export
                        PageCount = 1    // export a single page
                    };

                    // Build output file name (e.g., page_1.html, page_2.html, ...)
                    string outputFile = $"page_{i + 1}.html";

                    // Save the current page as HTML using the configured options
                    diagram.Save(outputFile, htmlOptions);

                    Console.WriteLine($"Saved page {i + 1} to {outputFile}");
                }
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}