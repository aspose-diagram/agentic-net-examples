using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.AutoLayout;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> [outputPdfFile]");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Determine output PDF path.
        string outputPath;
        if (args.Length >= 2 && !string.IsNullOrWhiteSpace(args[1]))
        {
            outputPath = args[1];
        }
        else
        {
            // Replace original extension with .pdf in the same folder.
            outputPath = Path.ChangeExtension(inputPath, ".pdf");
        }

        try
        {
            // Load the Visio diagram inside a using block (mandatory pattern).
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Perform auto‑spacing on each page before export.
                AutoSpaceOptions autoSpaceOptions = new AutoSpaceOptions();
                // Example spacing values; adjust as needed.
                autoSpaceOptions.DistanceInHorizontal = 0.5;
                autoSpaceOptions.DistanceInVertical = 0.5;

                foreach (Page page in diagram.Pages)
                {
                    // Auto‑space all shapes on the current page.
                    page.AutoSpaceShapes(page.Shapes, autoSpaceOptions);
                }

                // Configure PDF save options.
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;          // Explicitly set format tracker.
                pdfOptions.ExportHiddenPage = false;                // Do not export hidden pages.
                pdfOptions.DefaultFont = "Arial";                   // Fallback font if needed.

                // Save the diagram as PDF using the configured options.
                diagram.Save(outputPath, pdfOptions);
            }

            Console.WriteLine($"Diagram successfully exported to PDF: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error during PDF export: {ex.Message}");
        }
    }
}