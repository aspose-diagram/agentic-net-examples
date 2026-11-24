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
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputPdfFile>");
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
            // Load the Visio diagram inside a using block (Diagram implements IDisposable).
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Enable the display of page breaks in the first window (if any).
                if (diagram.Windows.Count > 0)
                {
                    Window window = diagram.Windows[0];
                    window.ShowPageBreaks = BOOL.True; // Show page breaks on the UI.
                }
                else
                {
                    Console.Error.WriteLine("The diagram contains no windows.");
                }

                // Configure PDF save options for preview generation.
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;               // Explicitly set format tracker.
                pdfOptions.TextCompression = PdfTextCompression.Flate;   // Apply flate compression to reduce size.

                // Save the diagram as a PDF preview.
                diagram.Save(outputPath, pdfOptions);
            }

            Console.WriteLine($"PDF preview generated successfully at: {outputPath}");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during loading or saving.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}