using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class ExportDiagramToPdf
{
    static void Main()
    {
        try
        {

            // Path to the source Visio diagram (already modified as needed)
            string sourceFile = @"C:\Diagrams\UpdatedDiagram.vsdx";

            // Path where the PDF will be saved
            string pdfFile = @"C:\Diagrams\UpdatedDiagram.pdf";

            // Load the diagram using the standard constructor (lifecycle rule)
            Diagram diagram = new Diagram(sourceFile);

            // Create PDF save options (rule-provided class)
            PdfSaveOptions pdfOptions = new PdfSaveOptions();

            // Optional: explicitly set the format to PDF (default is PDF)
            pdfOptions.SaveFormat = SaveFileFormat.Pdf;

            // Save the diagram as PDF using the Save method with SaveOptions (rule-provided)
            diagram.Save(pdfFile, pdfOptions);

            // Clean up resources
            diagram.Dispose();

            Console.WriteLine("Diagram exported to PDF successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
