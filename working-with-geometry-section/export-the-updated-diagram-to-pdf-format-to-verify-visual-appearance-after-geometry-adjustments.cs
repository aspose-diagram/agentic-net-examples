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

            // Load the Visio diagram (replace with your actual file path)
            Diagram diagram = new Diagram("input.vsdx");

            // TODO: Perform any geometry adjustments on the diagram here

            // Create PDF save options (default settings can be used or customized)
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            // Example of a custom option:
            // pdfOptions.EnlargePage = true;

            // Export the diagram to PDF to verify visual appearance
            diagram.Save("output.pdf", pdfOptions);

            // Clean up resources
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
