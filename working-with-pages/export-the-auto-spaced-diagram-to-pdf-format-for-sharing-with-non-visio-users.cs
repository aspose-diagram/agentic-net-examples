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

            // Path to the source Visio file (VSD, VDX, VSDX, etc.)
            string sourceFile = "input.vsdx";

            // Desired output PDF file path
            string outputPdf = "output.pdf";

            // Load the diagram from the file
            Diagram diagram = new Diagram(sourceFile);

            // Configure PDF save options (optional customizations)
            PdfSaveOptions pdfOptions = new PdfSaveOptions
            {
                // Export all pages (default behavior)
                PageCount = int.MaxValue,
                // Example: set PDF compliance level if needed
                // Compliance = Aspose.Diagram.Saving.PdfCompliance.Pdf15,
                // Example: enlarge page to fit drawing content
                // EnlargePage = true
            };

            // Save the diagram as PDF using the specified options
            diagram.Save(outputPdf, pdfOptions);

            // Release resources
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
