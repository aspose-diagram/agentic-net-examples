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

            // Load the diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Corporate standard margins (in inches)
                double leftMargin = 0.5;
                double rightMargin = 0.5;
                double topMargin = 0.75;
                double bottomMargin = 0.75;

                // Apply margins to every page
                foreach (Page page in diagram.Pages)
                {
                    var printProps = page.PageSheet.PrintProps;
                    printProps.PageLeftMargin.Value = leftMargin;
                    printProps.PageRightMargin.Value = rightMargin;
                    printProps.PageTopMargin.Value = topMargin;
                    printProps.PageBottomMargin.Value = bottomMargin;
                }

                // Configure PDF save options
                PdfSaveOptions pdfOptions = new PdfSaveOptions
                {
                    DefaultFont = "Arial",
                    SaveFormat = SaveFileFormat.Pdf
                };

                // Save the diagram as PDF with the custom margins
                string outputPath = "output.pdf";
                diagram.Save(outputPath, pdfOptions);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
