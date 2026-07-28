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
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath)) { Console.Error.WriteLine($"File not found: {inputPath}"); return; }

        // Path for the resulting PDF
        string outputPath = "output.pdf";

        try
        {
            // Load the diagram from the file
            Diagram diagram = new Diagram(inputPath);

            // Hide all background pages by setting UIVisibility to Hidden
            foreach (Page page in diagram.Pages)
            {
                // Identify background pages
                if (page.Background == BOOL.True)
                {
                    // Hide the page (UIVisibility.Hidden hides the page in the UI)
                    page.PageSheet.PageProps.UIVisibility.Value = UIVisibilityValue.Hidden;
                }
            }

            // Configure PDF save options to exclude hidden pages
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.ExportHiddenPage = false; // Do not export hidden pages

            // Save the diagram as PDF using the configured options
            diagram.Save(outputPath, pdfOptions);
        }
        catch (Exception ex)
        {
            // Report any errors to the error stream
            Console.Error.WriteLine("Error: " + ex.Message);
            throw;
        }
    }
}