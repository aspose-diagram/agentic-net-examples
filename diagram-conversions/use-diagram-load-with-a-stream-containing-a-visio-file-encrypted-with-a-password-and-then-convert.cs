using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Path to the encrypted Visio file (password handling is not supported via API)
        string inputPath = "encrypted.vsdx";
        // Guard: ensure the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Desired output PDF file path
        string outputPath = "converted.pdf";

        try
        {
            // Load the Visio diagram from a file stream using the constructor (Diagram.Load does not exist)
            using (FileStream stream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
            {
                Diagram diagram = new Diagram(stream);

                // Configure PDF save options
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.SaveFormat = SaveFileFormat.Pdf; // specify PDF format
                pdfOptions.DefaultFont = "Arial"; // fallback font if diagram fonts are missing

                // Save the diagram as PDF using the options object
                diagram.Save(outputPath, pdfOptions);
            }

            Console.WriteLine("Conversion completed.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}