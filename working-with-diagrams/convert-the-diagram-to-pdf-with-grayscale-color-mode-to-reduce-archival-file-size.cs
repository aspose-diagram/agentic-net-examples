using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: DiagramToPdf <inputVisioPath> <outputPdfPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Configure PDF save options.
                PdfSaveOptions pdfOptions = new PdfSaveOptions();

                // Set the default font to ensure consistent rendering.
                pdfOptions.DefaultFont = "Arial";

                // Explicitly set the save format tracker.
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;

                // NOTE: Aspose.Diagram does not provide a direct grayscale export option.
                // The PDF will be saved in full color. To achieve grayscale, a post‑processing
                // step with a PDF library would be required, which is beyond the scope here.

                // Save the diagram as PDF using the configured options.
                diagram.Save(outputPath, pdfOptions);
            }

            Console.WriteLine($"Diagram successfully converted to PDF: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}