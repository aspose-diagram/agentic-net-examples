using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define output PDF path
        string pdfPath = "protected.pdf";

        // Create diagram, add a rectangle, and save as password‑protected PDF
        try
        {
            // Instantiate a new empty diagram
            var diagram = new Diagram();

            // Get the active page to draw on
            var page = diagram.ActivePage;

            // Draw a rectangle: (pinX, pinY) = (5,5), width = 2, height = 1
            // Updated to use the non‑obsolete overload (x1, y1, x2, y2)
            page.DrawRectangle(5, 5, 7, 6); // x2 = 5+2, y2 = 5+1

            // Configure PDF save options with encryption
            var pdfOptions = new PdfSaveOptions();
            pdfOptions.EncryptionDetails = new PdfEncryptionDetails(
                "user123",          // user password
                "owner123",         // owner password
                PdfEncryptionAlgorithm.RC4_128);

            // Save the diagram as a protected PDF
            diagram.Save(pdfPath, pdfOptions);
            Console.WriteLine($"PDF saved with password protection to {pdfPath}");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during diagram creation or saving
            Console.Error.WriteLine("Error during diagram creation or PDF save: " + ex.Message);
            return;
        }

        // Verify the PDF file exists before attempting to open it
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Attempt to open the PDF without providing a password (should fail)
        try
        {
            // Use fully qualified Aspose.Pdf type to avoid namespace ambiguity
            var doc = new Aspose.Pdf.Document(pdfPath);
            // If no exception is thrown, the protection is not effective
            throw new Exception("PDF opened without password; protection failed.");
        }
        catch (Exception ex)
        {
            // Expected failure due to missing password
            Console.WriteLine("Opening without password failed as expected: " + ex.Message);
        }

        // Attempt to open the PDF with the correct user password (should succeed)
        try
        {
            // Use the overload that accepts a password string
            var doc = new Aspose.Pdf.Document(pdfPath, "user123");
            Console.WriteLine("PDF opened successfully with the correct password.");
        }
        catch (Exception ex)
        {
            // Propagate failure with a clear message
            throw new Exception("Failed to open PDF with correct password: " + ex.Message);
        }
    }
}