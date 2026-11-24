using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define input and output file paths (could be passed via args)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output.pdf";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Outer try-finally to guarantee disposal (using block handles actual disposal)
        try
        {
            // Use a using block to instantiate Diagram as required by the API guidelines
            using (Diagram diagram = new Diagram(inputPath))
            {
                try
                {
                    // Configure PDF save options
                    PdfSaveOptions pdfOptions = new PdfSaveOptions();
                    // Export hidden pages if needed
                    pdfOptions.ExportHiddenPage = true;
                    // Set a default fallback font
                    pdfOptions.DefaultFont = "Arial";

                    // Save the diagram as PDF using the configured options
                    diagram.Save(outputPath, pdfOptions);
                    Console.WriteLine($"Diagram successfully saved to {outputPath}");
                }
                catch (Exception ex)
                {
                    // Capture any Aspose.Diagram processing errors
                    Console.Error.WriteLine($"Error during diagram processing: {ex.Message}");
                }
            }
        }
        finally
        {
            // No explicit disposal needed; the using statement ensures Diagram is disposed.
        }
    }
}