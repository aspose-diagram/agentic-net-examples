using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Paths for the temporary Visio file and the resulting PDF
        string visioPath = "sample.vsdx";
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"File not found: {visioPath}");
            return;
        }

        string pdfPath = "protected.pdf";

        // Create a simple diagram (empty diagram with a default page) and save as protected PDF
        try
        {
            using (Diagram diagram = new Diagram())
            {
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.EncryptionDetails = new PdfEncryptionDetails(
                    "user123",
                    "owner123",
                    PdfEncryptionAlgorithm.RC4_128);

                diagram.Save(pdfPath, pdfOptions);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine("Error during PDF creation: " + ex.Message);
            return;
        }

        // Verify that opening the PDF without a password fails
        try
        {
            // Attempt to load the encrypted PDF without providing a password
            // This should throw an exception
            var doc = new Aspose.Pdf.Document(pdfPath);
            Console.WriteLine("Unexpectedly opened the PDF without a password.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Failed to open PDF without password as expected.");
            Console.WriteLine("Exception message: " + ex.Message);
        }

        // Clean up temporary files (optional)
        // File.Delete(visioPath);
        // File.Delete(pdfPath);
    }
}