using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class VisioToPdfBatch
{
    static void Main(string[] args)
    {
        try
        {

            // Path to the source Visio file
            string visioPath = "input.vsdx";

            // Folder where individual PDF files will be saved
            string outputFolder = "output";
            Directory.CreateDirectory(outputFolder);

            // Load the Visio diagram (uses Diagram(string) constructor)
            Diagram diagram = new Diagram(visioPath);

            // Loop through each page in the diagram
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                // Configure PDF save options to render only the current page
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.PageIndex = i;      // zero‑based index of the page to render
                pdfOptions.PageCount = 1;      // render a single page
                pdfOptions.SplitMultiPages = false; // keep the page as one PDF file

                // Build the output PDF file name (e.g., Page_1.pdf, Page_2.pdf, ...)
                string pdfPath = Path.Combine(outputFolder, $"Page_{i + 1}.pdf");

                // Save the selected page as a PDF (uses Diagram.Save(string, SaveOptions))
                diagram.Save(pdfPath, pdfOptions);
            }

            // Release resources
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
