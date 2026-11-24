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
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Output PDF file path
        string outputPath = "output.pdf";

        try
        {
            // Load the Visio diagram inside a using block (required pattern)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages to find background pages
                foreach (Page page in diagram.Pages)
                {
                    // Identify background pages
                    if (page.Background == BOOL.True)
                    {
                        // Hide the background page by setting UIVisibility to Hidden
                        page.PageSheet.PageProps.UIVisibility.Value = UIVisibilityValue.Hidden;
                    }
                }

                // Configure PDF save options to exclude hidden pages from export
                PdfSaveOptions pdfOptions = new PdfSaveOptions
                {
                    ExportHiddenPage = false,
                    SaveFormat = SaveFileFormat.Pdf
                };

                // Save the diagram to PDF using the configured options
                diagram.Save(outputPath, pdfOptions);
            }

            Console.WriteLine($"Diagram saved successfully to {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}