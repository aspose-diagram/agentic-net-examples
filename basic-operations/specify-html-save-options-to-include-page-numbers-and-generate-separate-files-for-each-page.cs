using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Path to the source Visio file (modify as needed)
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // -----------------------------------------------------------------
            // Add page numbers to the footer using Visio field codes.
            // '&p' inserts the current page number.
            // -----------------------------------------------------------------
            diagram.HeaderFooter.FooterRight = "Page: &p";

            // -----------------------------------------------------------------
            // Configure HTML save options.
            // ExportHiddenPage and IsExportComments are common settings.
            // -----------------------------------------------------------------
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions
            {
                ExportHiddenPage = false,
                IsExportComments = false
                // If the API supports page splitting, you could enable it here:
                // SplitPages = true;
            };

            // -----------------------------------------------------------------
            // Define output directory for HTML files.
            // -----------------------------------------------------------------
            string outputDir = "HtmlOutput";
            if (!Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }

            // -----------------------------------------------------------------
            // Save each page as a separate HTML file.
            // -----------------------------------------------------------------
            int pageCount = diagram.Pages.Count;
            for (int i = 0; i < pageCount; i++)
            {
                // Set the page index to export only the current page.
                htmlOptions.PageIndex = i;
                htmlOptions.PageCount = 1;

                string outputPath = Path.Combine(outputDir, $"output_page_{i + 1}.html");
                diagram.Save(outputPath, htmlOptions);
                Console.WriteLine($"Saved page {i + 1} to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
