using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Path to the source Visio diagram
        string inputPath = "input.vsdx";
        // Guard to ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Desired PDF output path
        string outputPath = "output.pdf";

        try
        {
            // Load the Visio diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Configure PDF save options (fallback font for missing glyphs)
            PdfSaveOptions pdfOptions = new PdfSaveOptions
            {
                DefaultFont = "Arial"
                // AutoFitPageToDrawingContent and SaveFormat are omitted because they are not supported in this version
            };

            // Save the diagram as a PDF using the configured options
            diagram.Save(outputPath, pdfOptions);
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}