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

            // Path to the source Visio diagram
            string sourceFile = @"C:\Diagrams\sample.vsdx";

            // Path where the PDF snapshot will be saved
            string outputPdf = @"C:\Diagrams\sample_snapshot.pdf";

            // Load the diagram from file
            Diagram diagram = new Diagram(sourceFile);

            // Configure PDF save options to include comments as callout annotations
            PdfSaveOptions pdfOptions = new PdfSaveOptions
            {
                IsExportComments = true   // Enable exporting of comments
            };

            // Save the diagram as a PDF using the configured options
            diagram.Save(outputPdf, pdfOptions);

            // Clean up resources
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
