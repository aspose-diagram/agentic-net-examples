using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define input Visio file (protected diagram) and output PDF path
        string inputPath = "protected.vsdx";
        string outputPdfPath = "protected.pdf";

        // Guard: ensure the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Capture global protection settings before export (BOOL is an enum, no .Value)
            BOOL protectBkgnds = diagram.DocumentSettings.ProtectBkgnds;
            BOOL protectMasters = diagram.DocumentSettings.ProtectMasters;
            BOOL protectShapes = diagram.DocumentSettings.ProtectShapes;
            BOOL protectStyles = diagram.DocumentSettings.ProtectStyles;

            // Configure PDF save options
            PdfSaveOptions pdfOptions = new PdfSaveOptions
            {
                // Set a fallback font for any missing fonts during export
                DefaultFont = "Arial",
                // Explicitly specify the output format
                SaveFormat = SaveFileFormat.Pdf
                // Encryption can be added here if needed:
                // EncryptionDetails = new PdfEncryptionDetails("userPwd", "ownerPwd", PdfEncryptionAlgorithm.RC4_128)
            };

            // Export the diagram to PDF using the configured options
            diagram.Save(outputPdfPath, pdfOptions);

            // Verify that protection metadata remains unchanged after export
            if (diagram.DocumentSettings.ProtectBkgnds != protectBkgnds ||
                diagram.DocumentSettings.ProtectMasters != protectMasters ||
                diagram.DocumentSettings.ProtectShapes != protectShapes ||
                diagram.DocumentSettings.ProtectStyles != protectStyles)
            {
                throw new Exception("Protection metadata was altered during PDF export.");
            }

            Console.WriteLine("PDF export completed successfully. Protection metadata verified.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}