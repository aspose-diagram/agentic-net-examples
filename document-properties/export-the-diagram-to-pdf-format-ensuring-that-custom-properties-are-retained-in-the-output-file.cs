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
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // OPTIONAL: Display custom document properties count (they will be retained on export).
            int customPropCount = diagram.DocumentProps.CustomProps.Count;
            Console.WriteLine($"Diagram loaded. Custom properties count: {customPropCount}");

            // Configure PDF save options.
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            // Explicitly set the format tracker.
            pdfOptions.SaveFormat = SaveFileFormat.Pdf;
            // Example setting – do not export hidden pages.
            pdfOptions.ExportHiddenPage = false;

            // Export the diagram to PDF. Custom properties are preserved automatically.
            diagram.Save(outputPath, pdfOptions);

            // Clean up resources.
            diagram.Dispose();

            Console.WriteLine($"Diagram successfully exported to PDF: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}