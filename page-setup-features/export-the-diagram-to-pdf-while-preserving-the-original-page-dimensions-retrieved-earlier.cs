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

            // Assume original page dimensions were retrieved earlier.
            // For example, you might have stored them like:
            // double originalWidth = ...;
            // double originalHeight = ...;

            // Create PDF save options.
            PdfSaveOptions pdfOptions = new PdfSaveOptions();

            // Preserve the original page size by leaving PageSize as null.
            // When PageSize is null Aspose.Diagram uses the source diagram's page dimensions.
            pdfOptions.PageSize = null;

            // Render all pages of the diagram.
            pdfOptions.PageCount = diagram.Pages.Count;

            // Save the diagram to PDF while keeping the original page dimensions.
            diagram.Save("output.pdf", pdfOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
