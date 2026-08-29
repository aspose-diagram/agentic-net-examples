using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Input Visio file path (first argument) and output PDF path (second argument)
            string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
            string outputPath = args.Length > 1 ? args[1] : "output.pdf";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // 10 mm = 0.3937007874 inches
            double marginInches = 10.0 / 25.4;

            // Apply the margin to every page
            foreach (Page page in diagram.Pages)
            {
                var printProps = page.PageSheet.PrintProps;
                printProps.PageTopMargin.Value = marginInches;
                printProps.PageBottomMargin.Value = marginInches;
                printProps.PageLeftMargin.Value = marginInches;
                printProps.PageRightMargin.Value = marginInches;
            }

            // Configure PDF save options
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial";
            pdfOptions.SaveFormat = SaveFileFormat.Pdf;

            // Save the diagram as PDF
            diagram.Save(outputPath, pdfOptions);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
