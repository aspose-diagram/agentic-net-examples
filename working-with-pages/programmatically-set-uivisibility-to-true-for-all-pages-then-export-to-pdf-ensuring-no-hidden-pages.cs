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

            // Load the Visio diagram from a file
            string inputPath = "input.vsdx";
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure all pages are visible in the UI
                foreach (Page page in diagram.Pages)
                {
                    // Set UI visibility to Visible for each page
                    page.PageSheet.PageProps.UIVisibility.Value = UIVisibilityValue.Visible;
                }

                // Configure PDF save options to exclude hidden pages
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.ExportHiddenPage = false;

                // Export the diagram to PDF
                string outputPath = "output.pdf";
                diagram.Save(outputPath, pdfOptions);
            }

            Console.WriteLine("Diagram exported to PDF with all pages visible.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
