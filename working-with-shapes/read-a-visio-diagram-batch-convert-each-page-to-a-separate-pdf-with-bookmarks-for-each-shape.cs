using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class VisioToPdfBatch
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string visioPath = @"C:\Docs\sample.vsdx";

            // Folder where individual page PDFs will be saved
            string outputFolder = @"C:\Docs\PdfPages";

            // Load the Visio diagram (uses the Diagram(string) constructor)
            Diagram diagram = new Diagram(visioPath);

            // Ensure the output directory exists
            Directory.CreateDirectory(outputFolder);

            // Iterate through each page in the diagram
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                // Configure PDF save options to render only the current page
                PdfSaveOptions pdfOptions = new PdfSaveOptions
                {
                    PageIndex = i,   // zero‑based index of the page to render
                    PageCount = 1,   // render a single page
                    SplitMultiPages = false
                    // Bookmarks for each shape could be added here via a PageSavingCallback
                    // if a PDF library supporting bookmark creation is integrated.
                };

                // Build the output PDF file name (e.g., Page_1.pdf, Page_2.pdf, ...)
                string pdfPath = Path.Combine(outputFolder, $"Page_{i + 1}.pdf");

                // Save the selected page as a PDF (uses Diagram.Save(string, SaveOptions))
                diagram.Save(pdfPath, pdfOptions);
            }

            // Release resources
            diagram.Dispose();

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
