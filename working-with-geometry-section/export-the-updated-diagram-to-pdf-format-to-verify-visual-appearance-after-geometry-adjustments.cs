using System;
using System.IO;

// Assume the diagram has already been loaded and modified (e.g., geometry adjustments)
// Export the diagram to PDF to verify the visual appearance

using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class ExportDiagramToPdf
{
    static void Main()
    {
        try
        {

            // Load the existing Visio diagram (replace with your actual file path)
            string inputPath = @"C:\Diagrams\UpdatedDiagram.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Create PDF save options (default settings are sufficient for a basic export)
            PdfSaveOptions pdfOptions = new PdfSaveOptions();

            // Optionally, customize options here, for example:
            // pdfOptions.Compliance = Aspose.Diagram.Saving.PdfCompliance.Pdf15;
            // pdfOptions.EnlargePage = true;

            // Save the diagram as a PDF file
            string outputPdfPath = @"C:\Diagrams\UpdatedDiagram.pdf";
            diagram.Save(outputPdfPath, pdfOptions);

            // Clean up
            diagram.Dispose();

            Console.WriteLine("Diagram exported to PDF successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
