using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputFolder>");
            return;
        }

        string inputPath = args[0];
        string outputFolder = args[1];

        // Guard: check input file existence.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Guard: ensure output directory exists.
        if (!Directory.Exists(outputFolder))
        {
            try
            {
                Directory.CreateDirectory(outputFolder);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output directory: {ex.Message}");
                return;
            }
        }

        // Load the Visio diagram inside a using block as required.
        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Prepare a list of page indices for parallel processing.
                int pageCount = diagram.Pages.Count;
                List<int> pageIndices = new List<int>(pageCount);
                for (int i = 0; i < pageCount; i++)
                {
                    pageIndices.Add(i);
                }

                // Parallel conversion of each page to an individual PDF file.
                Parallel.ForEach(pageIndices, pageIndex =>
                {
                    try
                    {
                        // Retrieve the page (not strictly needed for saving, but kept for clarity).
                        Page page = diagram.Pages[pageIndex];

                        // Configure PDF save options to export only the current page.
                        PdfSaveOptions pdfOptions = new PdfSaveOptions();
                        pdfOptions.SaveFormat = SaveFileFormat.Pdf;      // Explicitly set format tracker.
                        pdfOptions.PageIndex = pageIndex;                // Zero‑based page index.
                        pdfOptions.PageCount = 1;                        // Export a single page.

                        // Build output file name (e.g., Page_1.pdf).
                        string outputPath = Path.Combine(outputFolder, $"Page_{pageIndex + 1}.pdf");

                        // Save the specific page as PDF.
                        diagram.Save(outputPath, pdfOptions);
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"Error processing page {pageIndex + 1}: {ex.Message}");
                    }
                });
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to process diagram: {ex.Message}");
        }
    }
}