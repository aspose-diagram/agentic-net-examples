using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> [outputPdfPath]");
            return;
        }

        string inputPath = args[0];
        // Guard: input file must exist
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Determine output PDF path (optional second argument)
        string outputPath = args.Length >= 2 ? args[1] : Path.ChangeExtension(inputPath, ".pdf");
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            try
            {
                Directory.CreateDirectory(outputDir);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output directory: {ex.Message}");
                return;
            }
        }

        try
        {
            // Load the protected Visio diagram inside a using block
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Capture original protection metadata (BOOL enum values)
                BOOL originalProtectBkgnds = diagram.DocumentSettings.ProtectBkgnds;
                BOOL originalProtectMasters = diagram.DocumentSettings.ProtectMasters;
                BOOL originalProtectShapes = diagram.DocumentSettings.ProtectShapes;
                BOOL originalProtectStyles = diagram.DocumentSettings.ProtectStyles;

                // Configure PDF save options
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;   // Explicit format tracker
                pdfOptions.ExportHiddenPage = false;          // Example setting

                // Export diagram to PDF
                diagram.Save(outputPath, pdfOptions);

                // Verify that protection metadata still persists after export
                BOOL afterProtectBkgnds = diagram.DocumentSettings.ProtectBkgnds;
                BOOL afterProtectMasters = diagram.DocumentSettings.ProtectMasters;
                BOOL afterProtectShapes = diagram.DocumentSettings.ProtectShapes;
                BOOL afterProtectStyles = diagram.DocumentSettings.ProtectStyles;

                bool protectionUnchanged =
                    originalProtectBkgnds == afterProtectBkgnds &&
                    originalProtectMasters == afterProtectMasters &&
                    originalProtectShapes == afterProtectShapes &&
                    originalProtectStyles == afterProtectStyles;

                if (!protectionUnchanged)
                {
                    throw new Exception("Protection metadata changed after PDF export.");
                }

                Console.WriteLine("PDF export completed successfully.");
                Console.WriteLine($"Output file: {outputPath}");
                Console.WriteLine("Protection metadata verified and unchanged.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}