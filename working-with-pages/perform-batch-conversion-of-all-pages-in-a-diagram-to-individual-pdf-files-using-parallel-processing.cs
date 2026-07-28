using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

// Implements parallel conversion of each page in a Visio diagram to a separate PDF file.
    class Program
    {
        static void Main(string[] args)
        {
            // Validate input arguments.
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: DiagramBatchPdfExport <inputVisioFile> <outputFolder>");
                return;
            }

            string inputPath = args[0];
            string outputFolder = args[1];

            // Ensure the output folder exists.
            if (!System.IO.Directory.Exists(outputFolder))
            {
                System.IO.Directory.CreateDirectory(outputFolder);
            }

            // Load the diagram.
            Diagram diagram = new Diagram(inputPath);

            // Collect pages into a typed list for Parallel.ForEach (type inference issue otherwise).
            List<Page> pages = new List<Page>();
            foreach (Page page in diagram.Pages)
            {
                pages.Add(page);
            }

            // Process each page in parallel.
            Parallel.ForEach(pages, page =>
            {
                try
                {
                    // Determine the page index (zero‑based) within the diagram.
                    int pageIndex = page.ID - 1; // Page IDs start at 1 and are sequential.

                    // Prepare PDF save options to export only this page.
                    PdfSaveOptions pdfOptions = new PdfSaveOptions
                    {
                        SaveFormat = SaveFileFormat.Pdf,
                        PageIndex = pageIndex,
                        PageCount = 1,
                        // Optional: set a default font to avoid missing‑font issues.
                        DefaultFont = "Arial"
                    };

                    // Build output file name using page name (fallback to index if name is empty).
                    string safePageName = string.IsNullOrWhiteSpace(page.Name) ? $"Page_{pageIndex + 1}" : page.Name;
                    // Remove any invalid file name characters.
                    foreach (char c in System.IO.Path.GetInvalidFileNameChars())
                    {
                        safePageName = safePageName.Replace(c, '_');
                    }

                    string outputPath = System.IO.Path.Combine(outputFolder, $"{safePageName}.pdf");

                    // Save the specific page as PDF.
                    diagram.Save(outputPath, pdfOptions);

                    Console.WriteLine($"Successfully exported page '{safePageName}' to PDF.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error exporting page ID {page.ID}: {ex.Message}");
                }
            });
        }
    }