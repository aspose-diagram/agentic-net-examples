using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (encrypted with a password). Password handling is not supported in this version.
        string inputPath = args.Length > 0 ? args[0] : "encrypted_input.vsdx";
        // Output file path (converted to PDF in this example).
        string outputPath = args.Length > 1 ? args[1] : "converted_output.pdf";

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram from a FileStream.
            using (FileStream stream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
            {
                // The Diagram constructor accepts a stream. Password-protected files cannot be opened with a password via Aspose.Diagram.
                Diagram diagram = new Diagram(stream);

                // Prepare PDF save options.
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                // Explicitly set the target format (required for some save pipelines).
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;

                // Save the diagram to the desired output format.
                diagram.Save(outputPath, pdfOptions);

                // Dispose the diagram to release resources.
                diagram.Dispose();
            }

            Console.WriteLine($"Conversion completed successfully. Output saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}