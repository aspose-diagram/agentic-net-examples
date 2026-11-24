using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Printing;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputPdfPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page to set dynamic margins based on page size
            foreach (Page page in diagram.Pages)
            {
                // Retrieve page dimensions (in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Calculate margins as 5% of the respective dimensions
                double leftMargin = pageWidth * 0.05;
                double rightMargin = pageWidth * 0.05;
                double topMargin = pageHeight * 0.05;
                double bottomMargin = pageHeight * 0.05;

                // Apply margins to the page's print properties
                page.PageSheet.PrintProps.PageLeftMargin.Value = leftMargin;
                page.PageSheet.PrintProps.PageRightMargin.Value = rightMargin;
                page.PageSheet.PrintProps.PageTopMargin.Value = topMargin;
                page.PageSheet.PrintProps.PageBottomMargin.Value = bottomMargin;
            }

            // Prepare PDF save options (optional: configure additional settings)
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.ExportHiddenPage = false; // Do not export hidden pages

            // Save the diagram as PDF with the configured margins
            diagram.Save(outputPath, pdfOptions);
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}