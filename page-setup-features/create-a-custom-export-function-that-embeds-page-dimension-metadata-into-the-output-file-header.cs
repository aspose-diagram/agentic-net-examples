using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output PDF file path
        if (args.Length != 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputPdfPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Variables to hold page dimensions (in inches)
        double pageWidth = 0;
        double pageHeight = 0;

        // Load diagram, extract dimensions, and export to PDF
        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram contains at least one page
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Access the first page to retrieve its dimensions (in inches)
                Page firstPage = diagram.Pages[0];
                pageWidth = firstPage.PageSheet.PageProps.PageWidth.Value;
                pageHeight = firstPage.PageSheet.PageProps.PageHeight.Value;

                // Prepare PDF save options
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                // Explicitly set the format tracker (required by Aspose.Diagram)
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;
                // Example: do not export hidden pages
                pdfOptions.ExportHiddenPage = false;

                // Save the diagram to PDF using the specified options
                diagram.Save(outputPath, pdfOptions);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during diagram loading or PDF export: {ex.Message}");
            return;
        }

        // Embed page dimension metadata into the generated PDF
        try
        {
            // Load the generated PDF using fully qualified Aspose.Pdf type to avoid namespace conflicts
            Aspose.Pdf.Document pdfDocument = new Aspose.Pdf.Document(outputPath);

            // Set custom metadata: embed page dimensions into the PDF title field
            pdfDocument.Info.Title = $"Page Dimensions: {pageWidth:0.##} x {pageHeight:0.##} inches";

            // Save the PDF back, overwriting the previous file
            pdfDocument.Save(outputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during PDF metadata embedding: {ex.Message}");
        }
    }
}