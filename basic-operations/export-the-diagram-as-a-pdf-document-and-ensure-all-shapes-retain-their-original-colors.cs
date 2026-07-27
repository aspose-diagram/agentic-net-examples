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
            string sourceFile = "input.vsdx";

            // Path where the PDF will be saved
            string pdfFile = "output.pdf";

            // Load the diagram (uses the Diagram(string) constructor)
            using (Diagram diagram = new Diagram(sourceFile))
            {
                // Configure PDF save options to keep original appearance
                PdfSaveOptions pdfOptions = new PdfSaveOptions
                {
                    // Ensure the format is PDF
                    SaveFormat = SaveFileFormat.Pdf,

                    // Do not exclude hidden pages or guide shapes
                    ExportHiddenPage = false,
                    ExportGuideShapes = false,

                    // Export comments if any (set to true to keep them, false to ignore)
                    IsExportComments = false,

                    // Render all foreground pages (including background shapes)
                    SaveForegroundPagesOnly = false
                };

                // Save the diagram as PDF using the save options (Diagram.Save(string, SaveOptions))
                diagram.Save(pdfFile, pdfOptions);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
