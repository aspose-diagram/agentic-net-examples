using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        string outputPath = "output.pdf";

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Hide all background pages
            foreach (Page page in diagram.Pages)
            {
                // Check if the page is a background page
                if (page.Background == BOOL.True)
                {
                    // Hide the page from UI (makes it invisible in exported output)
                    page.PageSheet.PageProps.UIVisibility.Value = UIVisibilityValue.Hidden;
                }
            }

            // Configure PDF save options to exclude hidden pages
            PdfSaveOptions pdfOptions = new PdfSaveOptions
            {
                ExportHiddenPage = false,   // Do not export hidden pages
                DefaultFont = "Arial"       // Fallback font for missing characters
            };

            // Save the diagram as PDF with the specified options
            diagram.Save(outputPath, pdfOptions);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
            return;
        }

        Console.WriteLine("Diagram processed and saved to PDF successfully.");
    }
}