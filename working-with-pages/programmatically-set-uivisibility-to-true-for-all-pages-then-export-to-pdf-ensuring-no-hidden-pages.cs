using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputPdfPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input Visio file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (required pattern)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Set UIVisibility to Visible for every page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // UIVisibility controls whether a page is hidden in the UI
                    page.PageSheet.PageProps.UIVisibility.Value = UIVisibilityValue.Visible;
                }

                // Configure PDF save options to exclude hidden pages
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.ExportHiddenPage = false;               // Do not export hidden pages
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;        // Explicitly set the format tracker

                // Save the diagram as PDF using the configured options
                diagram.Save(outputPath, pdfOptions);
            }

            Console.WriteLine($"PDF successfully saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}