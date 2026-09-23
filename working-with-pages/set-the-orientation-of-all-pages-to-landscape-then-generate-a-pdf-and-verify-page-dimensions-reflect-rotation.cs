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

            // Path to the source Visio file (replace with actual file path)
            string sourcePath = "input.vsdx";
            // Path for the generated PDF
            string pdfPath = "output.pdf";

            // Load the diagram
            Diagram diagram = new Diagram(sourcePath);

            // Set orientation of all pages to Landscape
            foreach (Page page in diagram.Pages)
            {
                page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;
            }

            // Verify that each page's dimensions reflect landscape orientation (width > height)
            foreach (Page page in diagram.Pages)
            {
                double width = page.PageSheet.PageProps.PageWidth.Value;
                double height = page.PageSheet.PageProps.PageHeight.Value;

                if (width <= height)
                {
                    throw new Exception($"Page '{page.Name}' does not have landscape dimensions after orientation change. Width: {width}, Height: {height}");
                }
            }

            // Prepare PDF save options
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            // Optional: set a default font to avoid missing font warnings
            pdfOptions.DefaultFont = "Arial";

            // Save the diagram as PDF
            diagram.Save(pdfPath, pdfOptions);

            // Clean up
            diagram.Dispose();

            Console.WriteLine("PDF generated successfully with all pages set to landscape orientation.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
