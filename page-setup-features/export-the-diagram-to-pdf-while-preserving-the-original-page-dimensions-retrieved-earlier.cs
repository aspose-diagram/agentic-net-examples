using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing;

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
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Retrieve dimensions of the first page (original page size) in inches.
                Page firstPage = diagram.Pages[0];
                double pageWidthInches = firstPage.PageSheet.PageProps.PageWidth.Value;
                double pageHeightInches = firstPage.PageSheet.PageProps.PageHeight.Value;

                // Convert inches to points (1 inch = 72 points) for the PDF area.
                float widthPoints = (float)(pageWidthInches * 72);
                float heightPoints = (float)(pageHeightInches * 72);

                // Prepare PDF save options.
                PdfSaveOptions pdfOptions = new PdfSaveOptions();

                // Explicitly set the format tracker.
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;

                // Preserve original page size by defining the export area.
                pdfOptions.Area = new RectangleF(0, 0, widthPoints, heightPoints);

                // Export hidden pages flag (plain bool, not BOOL).
                pdfOptions.ExportHiddenPage = false;

                // Save the diagram as PDF using the configured options.
                diagram.Save(outputPath, pdfOptions);
                Console.WriteLine($"Diagram successfully exported to PDF: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error during PDF export: {ex.Message}");
        }
    }
}