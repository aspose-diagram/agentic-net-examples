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

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Configure global font settings before loading the diagram.
            // Set a fallback font name to be used when a required font is missing.
            FontConfigs.DefaultFontName = "Arial";

            // Optionally add a custom font folder (e.g., system fonts). Guard against missing folder.
            string systemFontDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "Fonts");
            if (Directory.Exists(systemFontDir))
            {
                // The second argument indicates whether to search subfolders recursively.
                FontConfigs.SetFontFolder(systemFontDir, true);
            }

            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Configure PDF save options.
                PdfSaveOptions pdfOptions = new PdfSaveOptions();

                // Set the default fallback font.
                pdfOptions.DefaultFont = "Arial";

                // Do not export hidden pages. Use native bool instead of Aspose.Diagram.BOOL.
                pdfOptions.ExportHiddenPage = false; // Changed from BOOL.False to bool

                // Keep all pages in a single PDF file. Use native bool.
                pdfOptions.SplitMultiPages = false; // Changed from BOOL.False to bool

                // Set PDF/A‑1b compliance (optional, but a valid option).
                pdfOptions.Compliance = PdfCompliance.PdfA1b;

                // Save the diagram as PDF using the configured options.
                diagram.Save(outputPath, pdfOptions);
            }

            Console.WriteLine($"PDF saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
