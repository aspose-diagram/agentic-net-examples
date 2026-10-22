using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    // Object used to synchronize console output across parallel threads
    private static readonly object _consoleLock = new object();

    static void Main(string[] args)
    {
        // Expect three arguments: input folder, output folder, output format (Pdf or Png)
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <program> <inputFolder> <outputFolder> <outputFormat>");
            return;
        }

        string inputFolder = args[0];
        string outputFolder = args[1];
        string outputFormat = args[2].Trim().ToUpperInvariant();

        // Validate input folder
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Ensure output folder exists
        if (!Directory.Exists(outputFolder))
        {
            try
            {
                Directory.CreateDirectory(outputFolder);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output folder: {ex.Message}");
                return;
            }
        }

        // Gather Visio files (common extensions)
        string[] visioFiles = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly);
        visioFiles = Array.FindAll(visioFiles, f =>
        {
            string ext = Path.GetExtension(f).ToLowerInvariant();
            return ext == ".vsdx" || ext == ".vsd" || ext == ".vdx" || ext == ".vsx" || ext == ".vtx";
        });

        if (visioFiles.Length == 0)
        {
            Console.Error.WriteLine("No Visio files found in the input folder.");
            return;
        }

        // Process each file in parallel
        Parallel.ForEach(visioFiles, filePath =>
        {
            // Guard against missing file (should not happen after GetFiles, but added per rules)
            if (!File.Exists(filePath))
            {
                lock (_consoleLock)
                {
                    Console.Error.WriteLine($"File not found: {filePath}");
                }
                return;
            }

            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(filePath);
            string outputPath = Path.Combine(outputFolder, fileNameWithoutExt);

            try
            {
                // Load the diagram
                Diagram diagram = new Diagram(filePath);

                // Choose saving strategy based on requested format
                if (outputFormat == "PDF")
                {
                    // Ensure .pdf extension
                    string pdfPath = outputPath + ".pdf";

                    // Configure PDF save options with a page-saving callback for progress logging
                    PdfSaveOptions pdfOptions = new PdfSaveOptions();
                    pdfOptions.PageSavingCallback = new PdfPageLogger(fileNameWithoutExt);

                    // Save the diagram as PDF
                    diagram.Save(pdfPath, pdfOptions);

                    lock (_consoleLock)
                    {
                        Console.WriteLine($"Converted '{filePath}' to PDF successfully.");
                    }
                }
                else if (outputFormat == "PNG")
                {
                    // Ensure .png extension
                    string pngPath = outputPath + ".png";

                    // Configure image save options (export first page only)
                    ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);
                    imgOptions.PageIndex = 0;   // first page
                    imgOptions.PageCount = 1;   // single page

                    // Save the diagram as PNG
                    diagram.Save(pngPath, imgOptions);

                    lock (_consoleLock)
                    {
                        Console.WriteLine($"Converted '{filePath}' to PNG successfully.");
                    }
                }
                else
                {
                    lock (_consoleLock)
                    {
                        Console.Error.WriteLine($"Unsupported output format: {outputFormat}. Supported: Pdf, Png.");
                    }
                }

                // Dispose diagram to free resources
                diagram.Dispose();
            }
            catch (Exception ex)
            {
                lock (_consoleLock)
                {
                    Console.Error.WriteLine($"Error processing '{filePath}': {ex.Message}");
                }
            }
        });
    }

    // Implementation of IPageSavingCallback to log page start/end events during PDF export
    private class PdfPageLogger : IPageSavingCallback
    {
        private readonly string _documentName;

        public PdfPageLogger(string documentName)
        {
            _documentName = documentName;
        }

        // Called before a page starts saving
        public void PageStartSaving(PageStartSavingArgs args)
        {
            lock (_consoleLock)
            {
                Console.WriteLine($"[{_documentName}] Starting to save page {args.PageIndex + 1} of {args.PageCount}.");
            }
        }

        // Called after a page has been saved
        public void PageEndSaving(PageEndSavingArgs args)
        {
            lock (_consoleLock)
            {
                Console.WriteLine($"[{_documentName}] Finished saving page {args.PageIndex + 1} of {args.PageCount}.");
            }

            // Example: stop after first page (not required, but shows usage)
            // args.HasMorePages = false;
        }
    }
}