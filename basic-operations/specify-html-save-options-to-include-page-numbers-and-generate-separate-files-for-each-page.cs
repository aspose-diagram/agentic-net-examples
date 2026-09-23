using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Path to the source Visio file
        string inputPath = "input.vsdx";

        // Verify the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Insert automatic page number field in the footer (Visio field code &p)
            diagram.HeaderFooter.FooterRight = "Page: &p";

            // Determine total page count for later use
            int totalPages = diagram.Pages.Count;

            // Loop through each page and save it as a separate HTML file
            for (int i = 0; i < totalPages; i++)
            {
                // Configure HTML save options for the current page
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                {
                    // Export hidden pages is disabled (optional)
                    ExportHiddenPage = false,
                    // Specify which page to render (0‑based index)
                    PageIndex = i,
                    // Indicate total number of pages (required for proper navigation links)
                    PageCount = totalPages
                };

                // Build output file name that includes the page number
                string outputPath = $"output_page_{i + 1}.html";

                // Save the current page as HTML
                diagram.Save(outputPath, htmlOptions);
            }
        }
        catch (Exception ex)
        {
            // Write any unexpected errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}