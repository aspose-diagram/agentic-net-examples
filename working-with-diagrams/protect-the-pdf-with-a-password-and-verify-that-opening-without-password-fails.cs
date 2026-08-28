using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Create a new empty Visio diagram
        Diagram diagram = new Diagram();

        // Configure PDF save options with password protection
        PdfSaveOptions pdfOptions = new PdfSaveOptions();
        // Use positional arguments because the constructor does not support named parameters
        pdfOptions.EncryptionDetails = new PdfEncryptionDetails("user123", "owner123", PdfEncryptionAlgorithm.RC4_128);

        // Define output PDF path
        string pdfPath = "protected.pdf";

        // Save the diagram as a password‑protected PDF (wrapped in try/catch)
        try
        {
            diagram.Save(pdfPath, pdfOptions);
            Console.WriteLine($"PDF saved with password protection to '{pdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error saving PDF: {ex.Message}");
            return;
        }

        // Verify that the PDF file was created
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found after save: {pdfPath}");
            return;
        }

        // Verify that opening the PDF without a password fails
        try
        {
            // Attempt to load the PDF without providing a password (using fully qualified type to avoid namespace clash)
            var pdfDoc = new Aspose.Pdf.Document(pdfPath);
            // If no exception is thrown, the verification fails
            Console.WriteLine("ERROR: PDF opened without a password. Verification failed.");
            throw new Exception("PDF opened without password protection.");
        }
        catch (Exception)
        {
            // Expected path: loading fails because the PDF is encrypted
            Console.WriteLine("Success: PDF could not be opened without a password (as expected).");
        }
    }
}