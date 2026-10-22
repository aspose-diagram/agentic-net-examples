using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (modify as needed)
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        // Output PDF preview path
        string outputPath = "output_showpagebreaks_true.pdf";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure there is at least one window (Visio always has a default window)
                if (diagram.Windows.Count > 0)
                {
                    // Enable page breaks visibility in the window
                    diagram.Windows[0].ShowPageBreaks = BOOL.True;
                }
                else
                {
                    Console.Error.WriteLine("No windows found in the diagram.");
                    return;
                }

                // Configure PDF save options
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                // Explicitly set the format tracker
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;
                // Apply text compression to reduce file size (optional)
                pdfOptions.TextCompression = PdfTextCompression.Flate;

                // Save the diagram as a PDF preview
                diagram.Save(outputPath, pdfOptions);
                Console.WriteLine($"PDF preview saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}