using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <input-diagram-path>");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found at '{inputPath}'.");
            return;
        }

        // Define output file paths
        string pdfOutputPath = Path.ChangeExtension(inputPath, ".pdf");
        string xpsOutputPath = Path.ChangeExtension(inputPath, ".xps");

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // -----------------------------------------------------------------
                // Apply a simple theming operation.
                // Aspose.Diagram does not expose a direct theming API in this context,
                // so we set a default font as a basic visual theme.
                // -----------------------------------------------------------------
                // Note: This is a placeholder for more advanced theming if supported.
                // -----------------------------------------------------------------

                // -----------------------------------------------------------------
                // Export to PDF with PdfSaveOptions
                // -----------------------------------------------------------------
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.ExportHiddenPage = false;               // Do not export hidden pages
                pdfOptions.DefaultFont = "Arial";                  // Set a default font for consistency
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;        // Explicitly set the format tracker

                // Save the diagram as PDF
                diagram.Save(pdfOutputPath, pdfOptions);
                Console.WriteLine($"PDF exported successfully to: {pdfOutputPath}");

                // -----------------------------------------------------------------
                // Export to XPS with XPSSaveOptions
                // -----------------------------------------------------------------
                XPSSaveOptions xpsOptions = new XPSSaveOptions();
                xpsOptions.ExportHiddenPage = false;               // Do not export hidden pages
                xpsOptions.DefaultFont = "Arial";                  // Consistent default font
                xpsOptions.SaveFormat = SaveFileFormat.Xps;        // Explicitly set the format tracker

                // Save the diagram as XPS
                diagram.Save(xpsOutputPath, xpsOptions);
                Console.WriteLine($"XPS exported successfully to: {xpsOutputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}