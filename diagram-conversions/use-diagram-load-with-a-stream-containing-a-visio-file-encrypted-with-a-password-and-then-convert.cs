using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (encrypted file). Password handling is not supported,
                // so the file must be accessible without a password for this example.
                string inputPath = "encrypted_diagram.vsdx";

                // Output PDF file path.
                string outputPath = "converted_diagram.pdf";

                try
                {
                    // Load the Visio diagram from a file stream.
                    using (FileStream stream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
                    {
                        // The Diagram constructor loads the document from the stream.
                        Diagram diagram = new Diagram(stream);

                        // Prepare PDF save options (default options are sufficient for conversion).
                        PdfSaveOptions pdfOptions = new PdfSaveOptions();

                        // Save the diagram as a PDF file.
                        diagram.Save(outputPath, pdfOptions);
                    }

                    Console.WriteLine($"Diagram successfully converted and saved to '{outputPath}'.");
                }
                catch (Exception ex)
                {
                    // Output any errors that occur during loading or saving.
                    Console.WriteLine("An error occurred during conversion:");
                    Console.WriteLine(ex.Message);
                    throw;
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }