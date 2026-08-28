using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Input Visio file path
            string inputPath = "input.vsdx";

            // Output directory for per‑page PDFs
            string outputDir = "output";
            Directory.CreateDirectory(outputDir);

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Total number of pages to process
            int pageCount = diagram.Pages.Count;
            Console.WriteLine($"Total pages to convert: {pageCount}");

            // Iterate through each page and save it as a separate PDF
            for (int i = 0; i < pageCount; i++)
            {
                // Build output file name for the current page (1‑based index for readability)
                string outPath = Path.Combine(outputDir, $"page_{i + 1}.pdf");

                // Configure PDF save options to export only the current page
                PdfSaveOptions pdfOptions = new PdfSaveOptions
                {
                    PageIndex = i,   // Zero‑based page index
                    PageCount = 1    // Export a single page
                };

                // Save the current page as PDF
                diagram.Save(outPath, pdfOptions);

                // Update console progress bar
                ShowProgress(i + 1, pageCount);
            }

            // Move to next line after progress bar is complete
            Console.WriteLine("\nConversion completed.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Simple console progress bar
    static void ShowProgress(int completed, int total)
    {
        const int barWidth = 50;
        double ratio = (double)completed / total;
        int filled = (int)(ratio * barWidth);

        Console.Write("\r[");
        Console.Write(new string('#', filled));
        Console.Write(new string('-', barWidth - filled));
        Console.Write($"] {completed}/{total} ({ratio:P0})");
    }
}
