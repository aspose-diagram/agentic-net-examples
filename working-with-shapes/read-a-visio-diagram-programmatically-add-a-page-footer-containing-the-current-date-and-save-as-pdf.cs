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
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputPdfPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Prepare the footer text with the current date.
                string currentDate = DateTime.Now.ToString("d"); // e.g., 10/21/2026

                // Set the footer (centered) for all pages.
                diagram.HeaderFooter.FooterCenter = currentDate;

                // Optionally adjust footer margin (in inches). Here we keep the default.

                // Configure PDF save options.
                PdfSaveOptions pdfOptions = new PdfSaveOptions();

                // Explicitly set the format tracker.
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;

                // Save the diagram as PDF using the options.
                diagram.Save(outputPath, pdfOptions);
            }

            Console.WriteLine($"Diagram successfully saved as PDF to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}