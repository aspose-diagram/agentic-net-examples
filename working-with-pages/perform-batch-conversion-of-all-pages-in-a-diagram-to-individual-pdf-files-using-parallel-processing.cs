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
            // Validate arguments
            if (args.Length < 1)
            {
                Console.WriteLine("Usage: DiagramBatchPdfExport <inputVisioFile> [outputFolder]");
                return;
            }

            string inputPath = args[0];
            if (!File.Exists(inputPath))
            {
                Console.WriteLine($"Input file not found: {inputPath}");
                return;
            }

            // Determine output folder
            string outputFolder = args.Length >= 2 ? args[1] : Path.GetDirectoryName(inputPath);
            if (!Directory.Exists(outputFolder))
            {
                Directory.CreateDirectory(outputFolder);
            }

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Collect pages into a typed list for Parallel.ForEach
            List<Page> pages = new List<Page>();
            foreach (Page page in diagram.Pages)
            {
                pages.Add(page);
            }

            // Parallel processing of each page
            Parallel.ForEach(pages, page =>
            {
                try
                {
                    // Prepare PDF save options for the specific page
                    PdfSaveOptions pdfOptions = new PdfSaveOptions
                    {
                        // PageIndex is zero‑based; Visio page IDs start at 1
                        PageIndex = (int)page.ID - 1,
                        // Export only the current page
                        PageCount = 1,
                        // Use a fallback font in case the diagram references missing fonts
                        DefaultFont = "Arial"
                    };

                    // Build output file name using page name (fallback to ID)
                    string safePageName = string.IsNullOrWhiteSpace(page.Name) ? $"Page_{page.ID}" : page.Name;
                    // Remove any invalid file name characters
                    foreach (char c in Path.GetInvalidFileNameChars())
                    {
                        safePageName = safePageName.Replace(c, '_');
                    }

                    string outputPath = Path.Combine(outputFolder, $"{safePageName}.pdf");

                    // Save only the selected page as PDF
                    diagram.Save(outputPath, pdfOptions);

                    Console.WriteLine($"Saved page '{page.Name}' to '{outputPath}'.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing page ID {page.ID}: {ex.Message}");
                }
            });
        }
    }