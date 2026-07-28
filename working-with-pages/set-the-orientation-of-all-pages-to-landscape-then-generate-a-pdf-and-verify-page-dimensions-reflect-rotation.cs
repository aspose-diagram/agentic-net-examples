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

            // Load an existing Visio diagram (replace with your actual file path)
            using (Diagram diagram = new Diagram("input.vsdx"))
            {
                // Set orientation of every page to Landscape
                foreach (Page page in diagram.Pages)
                {
                    page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;
                }

                // Verify that all pages are set to Landscape
                foreach (Page page in diagram.Pages)
                {
                    if (page.PageSheet.PrintProps.PrintPageOrientation.Value != PrintPageOrientationValue.Landscape)
                    {
                        throw new Exception($"Page '{page.Name}' orientation is not Landscape.");
                    }

                    // Output page dimensions for verification (in inches)
                    double width = page.PageSheet.PageProps.PageWidth.Value;
                    double height = page.PageSheet.PageProps.PageHeight.Value;
                    Console.WriteLine($"Page '{page.Name}': Width = {width} in, Height = {height} in, Orientation = Landscape");
                }

                // Prepare PDF save options
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.DefaultFont = "Arial";

                // Save the diagram as PDF
                diagram.Save("output.pdf", pdfOptions);
                Console.WriteLine("Diagram saved as PDF with Landscape orientation.");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
