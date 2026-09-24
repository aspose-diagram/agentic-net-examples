using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Input Visio file and output combined PDF path
            string inputPath = "input.vsdx";
            string outputPdfPath = "combined.pdf";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Create a temporary folder to store individual shape PDFs
            string tempFolder = Path.Combine(Path.GetTempPath(), "ShapePdfExport");
            Directory.CreateDirectory(tempFolder);

            List<string> shapePdfFiles = new List<string>();

            // Iterate through each page and each shape on the page
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    // Build a unique file name for the shape PDF
                    string shapePdfPath = Path.Combine(
                        tempFolder,
                        $"shape_page{page.ID}_shape{shape.ID}.pdf");

                    // Export the shape as a vector PDF page
                    shape.ToPdf(shapePdfPath);

                    shapePdfFiles.Add(shapePdfPath);
                }
            }

            // Combine all individual shape PDFs into a single PDF document
            Aspose.Pdf.Document combinedDoc = new Aspose.Pdf.Document();

            foreach (string pdfFile in shapePdfFiles)
            {
                Aspose.Pdf.Document srcDoc = new Aspose.Pdf.Document(pdfFile);
                foreach (Aspose.Pdf.Page srcPage in srcDoc.Pages)
                {
                    combinedDoc.Pages.Add(srcPage);
                }
            }

            // Save the combined PDF
            combinedDoc.Save(outputPdfPath);

            // Clean up temporary files and folder
            foreach (string pdfFile in shapePdfFiles)
            {
                try { File.Delete(pdfFile); } catch { }
            }
            try { Directory.Delete(tempFolder, true); } catch { }

            Console.WriteLine($"Combined PDF saved to: {outputPdfPath}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
