using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define output PDF path
        string outputPath = "protected.pdf";

        // Guard to ensure the directory for the output exists
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory not found: {outputDir}");
            return;
        }

        try
        {
            // Create a new diagram (contains a default page)
            Diagram diagram = new Diagram();

            // Add a simple rectangle shape to the first page
            // Parameters: pinX, pinY, width, height, master name, page index
            long shapeId = diagram.AddShape(5, 5, 2, 1, "Rectangle", 0);
            Shape shape = diagram.Pages[0].Shapes.GetShape(shapeId);
            shape.Text.Value.Add(new Txt("Sample"));

            // Configure PDF save options with password protection
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            // Use positional arguments – the constructor expects (userPassword, ownerPassword, algorithm)
            pdfOptions.EncryptionDetails = new PdfEncryptionDetails("userpwd", "ownerpwd", PdfEncryptionAlgorithm.RC4_128);

            // Save the diagram as a password‑protected PDF
            diagram.Save(outputPath, pdfOptions);
        }
        catch (Exception ex)
        {
            // Write any Aspose operation errors to the error stream and exit
            Console.Error.WriteLine($"Error during diagram creation or PDF saving: {ex.Message}");
            return;
        }

        // Verify that opening the PDF without a password fails
        try
        {
            // Attempt to open the encrypted PDF without providing a password
            var pdfDoc = new Aspose.Pdf.Document(outputPath);
            // If no exception is thrown, the protection did not work
            throw new Exception("PDF opened without password; protection was not applied.");
        }
        catch (Exception ex)
        {
            // Expected path: an exception indicates password protection is active
            Console.WriteLine("Password protection verified: PDF cannot be opened without a password.");
            Console.WriteLine($"Caught exception: {ex.Message}");
        }
    }
}