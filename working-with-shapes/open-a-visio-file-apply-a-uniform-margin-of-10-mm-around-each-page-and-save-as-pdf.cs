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
            string inputPath = "input.vsdx";

            // Path for the resulting PDF file
            string outputPath = "output.pdf";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Convert 10 mm to inches (1 inch = 25.4 mm)
            double marginInches = 10.0 / 25.4; // ≈0.3937 inches

            // Apply the same margin to every page in the diagram
            foreach (Page page in diagram.Pages)
            {
                var printProps = page.PageSheet.PrintProps;
                printProps.PageTopMargin.Value = marginInches;
                printProps.PageBottomMargin.Value = marginInches;
                printProps.PageLeftMargin.Value = marginInches;
                printProps.PageRightMargin.Value = marginInches;
            }

            // Configure PDF save options (optional: set a default font)
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial";

            // Save the diagram as a PDF file
            diagram.Save(outputPath, pdfOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
