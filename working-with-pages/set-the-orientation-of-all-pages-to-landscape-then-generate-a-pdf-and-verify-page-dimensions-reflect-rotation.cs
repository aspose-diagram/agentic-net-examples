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

            // Load an existing Visio diagram (replace with your file path)
            string inputPath = "input.vsdx";
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Set orientation of every page to Landscape
                foreach (Page page in diagram.Pages)
                {
                    page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;
                }

                // Verify page dimensions (width should be greater than height for landscape)
                foreach (Page page in diagram.Pages)
                {
                    double width = page.PageSheet.PageProps.PageWidth.Value;
                    double height = page.PageSheet.PageProps.PageHeight.Value;
                    Console.WriteLine($"Page ID {page.ID}: Width = {width} in, Height = {height} in, Landscape = {width > height}");
                    if (width <= height)
                    {
                        throw new Exception($"Page {page.ID} is not in landscape orientation as expected.");
                    }
                }

                // Save the diagram as PDF
                string outputPath = "output.pdf";
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.DefaultFont = "Arial"; // Ensure font fallback
                diagram.Save(outputPath, pdfOptions);
            }

            Console.WriteLine("Diagram saved as PDF with all pages set to landscape.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
