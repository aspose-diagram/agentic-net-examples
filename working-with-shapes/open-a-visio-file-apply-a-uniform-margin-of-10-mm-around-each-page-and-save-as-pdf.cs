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

            // Input Visio file path
            string inputPath = "input.vsdx";
            // Output PDF file path
            string outputPath = "output.pdf";

            // Margin of 10 mm converted to inches (1 inch = 25.4 mm)
            double marginInches = 10.0 / 25.4; // ≈0.3937 inches

            // Load the Visio diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Apply the same margin to every page
                foreach (Page page in diagram.Pages)
                {
                    // PrintProps holds page margin settings (values are in inches)
                    page.PageSheet.PrintProps.PageTopMargin.Value = marginInches;
                    page.PageSheet.PrintProps.PageBottomMargin.Value = marginInches;
                    page.PageSheet.PrintProps.PageLeftMargin.Value = marginInches;
                    page.PageSheet.PrintProps.PageRightMargin.Value = marginInches;
                }

                // Configure PDF save options (optional: set a default font)
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.DefaultFont = "Arial";

                // Save the modified diagram as PDF
                diagram.Save(outputPath, pdfOptions);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
