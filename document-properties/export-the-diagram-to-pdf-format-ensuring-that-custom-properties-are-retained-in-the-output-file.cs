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

            // Path to the source Visio diagram
            string sourceFile = "input.vsdx";

            // Path for the resulting PDF file
            string pdfFile = "output.pdf";

            // Load the Visio diagram (custom properties are part of the diagram and will be retained)
            Diagram diagram = new Diagram(sourceFile);

            // Configure PDF save options (default settings retain all document data)
            PdfSaveOptions pdfOptions = new PdfSaveOptions();

            // Save the diagram as PDF using the configured options
            diagram.Save(pdfFile, pdfOptions);

            // Clean up resources
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
