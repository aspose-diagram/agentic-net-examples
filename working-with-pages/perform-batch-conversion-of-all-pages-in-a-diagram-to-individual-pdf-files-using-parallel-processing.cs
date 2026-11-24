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
        // Validate arguments: expect input Visio file path and output folder path.
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

        // Ensure output directory exists.
        try
        {
            Directory.CreateDirectory(outputFolder);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to create output directory '{outputFolder}': {ex.Message}");
            return;
        }

        // Load diagram inside a using block as required.
        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Collect pages into a list for Parallel.For (type inference issue with PageCollection).
                List<Page> pages = new List<Page>();
                foreach (Page p in diagram.Pages)
                {
                    pages.Add(p);
                }

                // Parallel processing of each page.
                Parallel.For(0, pages.Count, i =>
                {
                    try
                    {
                        Page page = pages[i];

                        // Prepare PDF save options for a single page.
                        PdfSaveOptions pdfOptions = new PdfSaveOptions();
                        pdfOptions.PageIndex = i;          // Zero‑based page index.
                        pdfOptions.PageCount = 1;          // Export only this page.
                        pdfOptions.SaveFormat = SaveFileFormat.Pdf;

                        // Build output file name (e.g., Page_1.pdf).
                        string outputPath = Path.Combine(outputFolder, $"Page_{i + 1}.pdf");

                        // Save the specific page to PDF.
                        diagram.Save(outputPath, pdfOptions);
                        Console.WriteLine($"Saved page {i + 1} to '{outputPath}'.");
                    }
                    catch (Exception exPage)
                    {
                        Console.Error.WriteLine($"Error processing page index {i}: {exPage.Message}");
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