using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> [outputPdfPath]");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Determine output path; if not provided, use same folder with .pdf extension
        string outputPath = args.Length >= 2 ? args[1] : Path.ChangeExtension(inputPath, ".pdf");

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Create PDF save options
                PdfSaveOptions pdfOptions = new PdfSaveOptions();

                // Ensure the save format is explicitly set (required for PDF export)
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;

                // By default, comments are exported as callout annotations.
                // No additional property is needed; if you need to hide them, you would use ImageSaveOptions.IsExportComments = false.

                // Save the diagram as PDF with comments rendered as callouts
                diagram.Save(outputPath, pdfOptions);
            }

            Console.WriteLine($"PDF saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error during PDF export: {ex.Message}");
        }
    }
}