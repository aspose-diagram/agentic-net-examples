using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Prompt user for the Visio file path
        Console.Write("Enter the full path to the Visio file: ");
        string visioPath = Console.ReadLine();

        // Guard: ensure the file path is not empty and the file exists
        if (string.IsNullOrWhiteSpace(visioPath) || !File.Exists(visioPath))
        {
            Console.Error.WriteLine($"File not found: {visioPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(visioPath);

            // Prepare output folder for the per‑page PDFs
            string outputFolder = Path.Combine(Path.GetDirectoryName(visioPath) ?? "", "PdfPages");
            Directory.CreateDirectory(outputFolder);

            // Iterate through each page in the diagram
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                // Retrieve the current page
                Page page = diagram.Pages[i];

                // Build a safe file name for the page PDF
                string pageName = string.IsNullOrWhiteSpace(page.NameU) ? $"Page_{page.ID}" : page.NameU;
                string safePageName = string.Concat(pageName.Split(Path.GetInvalidFileNameChars()));
                string pdfPath = Path.Combine(outputFolder, $"{safePageName}.pdf");

                // Configure PDF save options to export only the current page
                PdfSaveOptions pdfOptions = new PdfSaveOptions
                {
                    PageIndex = i,          // zero‑based index of the page to export
                    PageCount = 1,          // export a single page
                    ExportHiddenPage = false,
                    DefaultFont = "Arial"
                };
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;

                // Save the single page as PDF
                diagram.Save(pdfPath, pdfOptions);

                // NOTE: Adding bookmarks for each shape requires Aspose.Pdf's OutlineItem class.
                // The current project does not reference Aspose.Pdf, so bookmark creation is omitted
                // to keep the code compilable. If Aspose.Pdf is added, the bookmark logic can be
                // re‑introduced here.

                Console.WriteLine($"Exported page '{pageName}' to PDF: {pdfPath}");
            }

            Console.WriteLine("Batch conversion completed.");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}