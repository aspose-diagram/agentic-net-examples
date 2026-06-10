using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        string outputPath = "output.pdf";

        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Apply global read‑only protection to the document
                diagram.DocumentSettings.ProtectShapes = BOOL.True;
                diagram.DocumentSettings.ProtectMasters = BOOL.True;
                diagram.DocumentSettings.ProtectBkgnds = BOOL.True;
                diagram.DocumentSettings.ProtectStyles = BOOL.True;

                // Configure PDF export with password protection
                PdfSaveOptions pdfOptions = new PdfSaveOptions
                {
                    SaveFormat = SaveFileFormat.Pdf,
                    EncryptionDetails = new PdfEncryptionDetails("userPassword", "ownerPassword", PdfEncryptionAlgorithm.RC4_128)
                };

                // Save the protected diagram as a PDF
                diagram.Save(outputPath, pdfOptions);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}