using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Printing;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Input Visio file (adjust the path as needed)
            string inputPath = "input.vsdx";

            // Output PDF file after applying dynamic margins
            string outputPath = "output.pdf";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and set margins based on page size
            foreach (Page page in diagram.Pages)
            {
                // Retrieve page dimensions (in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Example: set margins to 5% of the respective dimension
                double leftRightMargin = pageWidth * 0.05;
                double topBottomMargin = pageHeight * 0.05;

                // Access the printing properties for the page
                PrintProps printProps = page.PageSheet.PrintProps;

                // Set margins (values are in inches)
                printProps.PageLeftMargin.Value = leftRightMargin;
                printProps.PageRightMargin.Value = leftRightMargin;
                printProps.PageTopMargin.Value = topBottomMargin;
                printProps.PageBottomMargin.Value = topBottomMargin;
            }

            // Prepare PDF save options (optional: set default font)
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial";

            // Save the modified diagram as PDF
            diagram.Save(outputPath, pdfOptions);

            // Clean up resources
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
