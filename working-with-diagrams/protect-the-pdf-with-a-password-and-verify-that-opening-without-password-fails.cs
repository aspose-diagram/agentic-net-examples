using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> [outputPdfPath]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args.Length > 1 ? args[1] : "protected.pdf";

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Define passwords for PDF protection.
        string userPassword = "user123";
        string ownerPassword = "owner123";

        // -----------------------------------------------------------------
        // Create the diagram and save it as a password‑protected PDF.
        // -----------------------------------------------------------------
        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Configure PDF save options with encryption details.
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.EncryptionDetails = new PdfEncryptionDetails(
                    userPassword,
                    ownerPassword,
                    PdfEncryptionAlgorithm.RC4_128);

                // Save the diagram to PDF using the configured options.
                diagram.Save(outputPath, pdfOptions);
                Console.WriteLine($"PDF saved with password protection to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during PDF creation: {ex.Message}");
            return;
        }

        // -----------------------------------------------------------------
        // Verify that opening the PDF without a password fails.
        // -----------------------------------------------------------------
        try
        {
            // Attempt to open the protected PDF without providing a password.
            // This should throw an exception.
            var pdfDoc = new Aspose.Pdf.Document(outputPath);
            // If no exception occurs, the verification has failed.
            Console.Error.WriteLine("Verification failed: PDF opened without a password.");
        }
        catch (Exception)
        {
            // Expected path: opening without a password throws an exception.
            Console.WriteLine("Verification passed: PDF cannot be opened without a password.");
        }
    }
}