using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (modify as needed)
        string inputVisioPath = "input.vsdx";
        if (!System.IO.File.Exists(inputVisioPath))
        {
            Console.Error.WriteLine($"File not found: {inputVisioPath}");
            return;
        }

        // Guard: ensure the input Visio file exists
        if (!File.Exists(inputVisioPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputVisioPath}");
            return;
        }

        // Output PDF file path
        string outputPdfPath = "protected.pdf";

        // Load the Visio diagram and save it as a password‑protected PDF
        try
        {
            using (Diagram diagram = new Diagram(inputVisioPath))
            {
                // Configure PDF save options with encryption details
                PdfSaveOptions pdfOptions = new PdfSaveOptions();

                // Set encryption details (user password, owner password, algorithm)
                pdfOptions.EncryptionDetails = new PdfEncryptionDetails(
                    "user123",
                    "owner123",
                    PdfEncryptionAlgorithm.RC4_128);

                // Save the diagram as PDF using the configured options
                diagram.Save(outputPdfPath, pdfOptions);
                Console.WriteLine($"PDF saved with password protection: {outputPdfPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during PDF creation: {ex.Message}");
            return;
        }

        // Verify that opening the PDF without a password fails
        try
        {
            // Attempt to load the protected PDF without providing a password
            var pdfDoc = new Aspose.Pdf.Document(outputPdfPath);
            // If no exception is thrown, the verification fails
            Console.WriteLine("Verification FAILED: PDF opened without a password.");
        }
        catch (Exception)
        {
            // Expected path: an exception indicates the PDF is password protected
            Console.WriteLine("Verification PASSED: PDF cannot be opened without a password.");
        }
    }
}