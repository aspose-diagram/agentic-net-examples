using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";
            // Path for the exported PDF
            string outputPath = "output.pdf";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Ensure all pages are visible in the UI
            foreach (Page page in diagram.Pages)
            {
                // Set UI visibility to visible for each page
                page.PageSheet.PageProps.UIVisibility.Value = UIVisibilityValue.Visible;
            }

            // Configure PDF save options to exclude hidden pages
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.ExportHiddenPage = false;          // Do not export hidden pages
            pdfOptions.DefaultFont = "Arial";             // Fallback font
            pdfOptions.SaveFormat = SaveFileFormat.Pdf;   // Explicitly set format

            // Save the diagram as PDF
            diagram.Save(outputPath, pdfOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
