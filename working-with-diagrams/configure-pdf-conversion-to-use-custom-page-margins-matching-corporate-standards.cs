using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string sourcePath = "input.vsdx";

            // Path for the resulting PDF file
            string outputPath = "output.pdf";

            // Load the diagram (ensure proper disposal)
            using (Diagram diagram = new Diagram(sourcePath))
            {
                // Corporate standard margins (in inches)
                double topMargin = 0.5;
                double bottomMargin = 0.5;
                double leftMargin = 0.5;
                double rightMargin = 0.5;

                // Apply margins to every page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    page.PageSheet.PrintProps.PageTopMargin.Value = topMargin;
                    page.PageSheet.PrintProps.PageBottomMargin.Value = bottomMargin;
                    page.PageSheet.PrintProps.PageLeftMargin.Value = leftMargin;
                    page.PageSheet.PrintProps.PageRightMargin.Value = rightMargin;
                }

                // Configure PDF save options (optional: set default font)
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.DefaultFont = "Arial";

                // Save the diagram as PDF with the configured options
                diagram.Save(outputPath, pdfOptions);
            }

            Console.WriteLine("PDF conversion completed with custom margins.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
