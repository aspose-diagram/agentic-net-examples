using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the encrypted Visio file
            string inputPath = "encrypted_input.vsdx";
            // Path for the converted output (e.g., PDF)
            string outputPath = "converted_output.pdf";

            // Initialize load options (no password property is available)
            LoadOptions loadOptions = new LoadOptions();

            // Load the diagram using the constructor that accepts LoadOptions.
            // Password handling for encrypted files is not supported via LoadOptions.
            Diagram diagram = new Diagram(inputPath, loadOptions);

            // Simple progress tracking: report each page processed.
            int totalPages = diagram.Pages.Count;
            Console.WriteLine($"Total pages to process: {totalPages}");

            for (int i = 0; i < totalPages; i++)
            {
                // Here you could perform per‑page operations.
                // For demonstration we just output progress.
                Console.WriteLine($"Processing page {i + 1} of {totalPages}...");
            }

            // Save the diagram to PDF (or any other supported format).
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine("Conversion completed successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
