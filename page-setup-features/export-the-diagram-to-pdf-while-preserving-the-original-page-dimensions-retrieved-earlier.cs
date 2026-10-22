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

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Retrieve and display original page dimensions (in inches).
            // Page dimensions are stored in PageSheet.PageProps.
            foreach (Page page in diagram.Pages)
            {
                double width = page.PageSheet.PageProps.PageWidth.Value;
                double height = page.PageSheet.PageProps.PageHeight.Value;
                Console.WriteLine($"Page \"{page.Name}\" - Width: {width} in, Height: {height} in");
            }

            // Configure PDF save options.
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            // Explicitly set the format tracker as required by the API.
            pdfOptions.SaveFormat = SaveFileFormat.Pdf;

            // Export the diagram to PDF while preserving original page dimensions.
            diagram.Save(outputPath, pdfOptions);

            // Clean up resources.
            diagram.Dispose();

            Console.WriteLine($"Diagram successfully exported to PDF: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error during PDF export: {ex.Message}");
        }
    }
}