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
            Diagram diagram = new Diagram("input.vsdx");

            // Set the global header margin to 0.5 inches
            // HeaderMargin is a Margin type; use its Value property (in inches)
            diagram.HeaderFooter.HeaderMargin.Value = 0.5;

            // Prepare PDF save options (optional: set a default font for missing fonts)
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial";

            // Export the diagram to PDF using the configured options
            diagram.Save("output.pdf", pdfOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
