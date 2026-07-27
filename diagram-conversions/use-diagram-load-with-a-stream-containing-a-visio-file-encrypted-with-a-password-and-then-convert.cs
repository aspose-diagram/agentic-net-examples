using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the encrypted Visio file (the password cannot be supplied; Aspose.Diagram does not support it)
            string inputPath = "encrypted_diagram.vsdx";
            // Output PDF path
            string outputPath = "converted_diagram.pdf";

            // Load the Visio file from a stream
            using (FileStream stream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
            {
                // The Diagram constructor loads the document from the stream.
                // Note: Encrypted files cannot be opened with a password using Aspose.Diagram.
                Diagram diagram = new Diagram(stream);

                // Prepare PDF save options (optional: set default font to avoid missing glyphs)
                PdfSaveOptions pdfOptions = new PdfSaveOptions
                {
                    DefaultFont = "Arial"
                };

                // Save the diagram as PDF
                diagram.Save(outputPath, pdfOptions);
            }

            Console.WriteLine("Conversion completed.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
