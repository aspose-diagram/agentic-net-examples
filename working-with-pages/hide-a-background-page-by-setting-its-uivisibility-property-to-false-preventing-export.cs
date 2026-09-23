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

        // Guard to ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Hide all background pages by setting UIVisibility to Hidden
            foreach (Page page in diagram.Pages)
            {
                // Identify background pages (Background == BOOL.True)
                if (page.Background == BOOL.True)
                {
                    // UIVisibilityValue.Hidden marks the page as hidden and prevents export
                    page.PageSheet.PageProps.UIVisibility.Value = UIVisibilityValue.Hidden;
                }
            }

            // Configure PDF save options to exclude hidden pages from export
            PdfSaveOptions pdfOptions = new PdfSaveOptions
            {
                ExportHiddenPage = false
            };

            // Save the modified diagram to PDF
            diagram.Save("output.pdf", pdfOptions);
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}