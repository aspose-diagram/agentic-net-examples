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

            // Path to the source Visio file
            string sourceFile = @"C:\Diagrams\sample.vsdx";

            // Desired PDF output path
            string pdfFile = @"C:\Diagrams\sample.pdf";

            // Load the Visio diagram
            Diagram diagram = new Diagram(sourceFile);

            // Configure PDF save options to preserve original appearance
            PdfSaveOptions pdfOptions = new PdfSaveOptions
            {
                // Ensure the format is set explicitly (PDF)
                SaveFormat = SaveFileFormat.Pdf,

                // Do not export hidden pages or guide shapes – only visible content
                ExportHiddenPage = false,
                ExportGuideShapes = false,

                // Preserve comments if present (set to true to keep them, false to ignore)
                IsExportComments = false,

                // Enlarge page if needed to fit all drawing content
                EnlargePage = true
            };

            // Save the diagram as PDF using the configured options
            diagram.Save(pdfFile, pdfOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
