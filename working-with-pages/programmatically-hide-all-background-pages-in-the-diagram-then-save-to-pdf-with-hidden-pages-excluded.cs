using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect input Visio file path and output PDF file path as arguments
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: HideBackgroundPages <inputVisioFile> <outputPdfFile>");
            return;
        }

        string inputPath = args[0];
        // Guard to ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        string outputPath = args[1];

        try
        {
            // Load the Visio diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Hide all background pages by setting UIVisibility to Hidden
            foreach (Page page in diagram.Pages)
            {
                if (page.Background == BOOL.True)
                {
                    // UIVisibility.Value expects a UIVisibilityValue enum, not BOOL
                    page.PageSheet.PageProps.UIVisibility.Value = UIVisibilityValue.Hidden;
                }
            }

            // Configure PDF save options to exclude hidden pages
            PdfSaveOptions pdfOptions = new PdfSaveOptions
            {
                ExportHiddenPage = false,   // Do not export hidden pages
                DefaultFont = "Arial"       // Provide a fallback font
            };

            // Save the diagram as PDF with hidden pages excluded
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine($"Diagram saved to PDF without hidden pages: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}