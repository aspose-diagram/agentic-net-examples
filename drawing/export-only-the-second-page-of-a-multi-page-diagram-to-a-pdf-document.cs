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
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output PDF file path
        string outputPath = "second_page.pdf";

        try
        {
            // Load the diagram inside a using block (required for proper disposal)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Verify that the diagram contains at least two pages
                if (diagram.Pages.Count < 2)
                {
                    Console.Error.WriteLine("The diagram does not contain a second page to export.");
                    return;
                }

                // Configure PDF save options to export only the second page (zero‑based index 1)
                PdfSaveOptions pdfOptions = new PdfSaveOptions();

                // Explicitly set the target format (required when using save options)
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;

                // Export only one page starting from the second page
                pdfOptions.PageIndex = 1;   // second page (zero‑based)
                pdfOptions.PageCount = 1;   // export a single page

                // Do not export hidden pages (use native bool, not BOOL enum)
                pdfOptions.ExportHiddenPage = false; // corrected type

                // Optional: set a default font to avoid missing‑font warnings
                pdfOptions.DefaultFont = "Arial";

                // Save the selected page to PDF
                diagram.Save(outputPath, pdfOptions);
                Console.WriteLine($"Second page exported successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
