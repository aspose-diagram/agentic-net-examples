using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Printing;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (first argument) and output PDF path (second argument)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output.pdf";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages to set dynamic margins based on page size
            foreach (Page page in diagram.Pages)
            {
                // Retrieve page width and height (values are in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Compute margins as 5% of the respective dimensions
                double leftRightMargin = pageWidth * 0.05;
                double topBottomMargin = pageHeight * 0.05;

                // Set the margins via PrintProps (values are in inches)
                page.PageSheet.PrintProps.PageLeftMargin.Value = leftRightMargin;
                page.PageSheet.PrintProps.PageRightMargin.Value = leftRightMargin;
                page.PageSheet.PrintProps.PageTopMargin.Value = topBottomMargin;
                page.PageSheet.PrintProps.PageBottomMargin.Value = topBottomMargin;
            }

            // OPTIONAL: If you need to send the diagram to a printer, you could use Aspose.Diagram.Printing APIs here.
            // For demonstration, we will save the diagram as a PDF which respects the margin settings.

            // Configure PDF save options (no special settings required for margins)
            PdfSaveOptions pdfOptions = new PdfSaveOptions();

            // Save the diagram to PDF with the configured margins
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine($"Diagram processed and saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}