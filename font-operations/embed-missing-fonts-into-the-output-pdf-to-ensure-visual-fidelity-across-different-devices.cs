using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing.Text;

class Program
{
    static void Main(string[] args)
    {
        // Expect at least two arguments: input Visio file path and output PDF file path.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputPdfPath> [customFontFolder]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string customFontFolder = args.Length >= 3 ? args[2] : null;

        // Guard: verify input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Guard: if a custom font folder is provided, verify it exists.
        if (!string.IsNullOrEmpty(customFontFolder) && !Directory.Exists(customFontFolder))
        {
            Console.Error.WriteLine($"Custom font folder does not exist: {customFontFolder}");
            return;
        }

        try
        {
            // Configure Aspose.Diagram to use a custom font folder if supplied.
            if (!string.IsNullOrEmpty(customFontFolder))
            {
                // Register the custom font folder (recursive search).
                FontConfigs.SetFontFolder(customFontFolder, true);
            }

            // Set a default fallback font name (must be installed or in the custom folder).
            FontConfigs.DefaultFontName = "Arial";

            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Prepare PDF save options.
            PdfSaveOptions pdfOptions = new PdfSaveOptions();

            // Set the default font for PDF rendering.
            pdfOptions.DefaultFont = "Arial";

            // Save the diagram to PDF with the specified options.
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine($"PDF saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Log any exceptions that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}