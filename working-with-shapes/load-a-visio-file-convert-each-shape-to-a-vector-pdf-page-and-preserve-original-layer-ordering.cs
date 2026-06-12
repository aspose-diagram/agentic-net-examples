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

            // Input Visio file path
            string inputVisioPath = "input.vsdx";

            // Temporary folder to store individual shape PDFs
            string tempFolder = Path.Combine(Path.GetTempPath(), "ShapePdfExport");
            Directory.CreateDirectory(tempFolder);

            // Load the source Visio diagram
            Diagram sourceDiagram = new Diagram(inputVisioPath);

            // List to keep track of generated shape PDF file paths in order
            List<string> shapePdfFiles = new List<string>();

            // Iterate pages preserving original order
            foreach (Page srcPage in sourceDiagram.Pages)
            {
                // Iterate shapes preserving original order
                foreach (Aspose.Diagram.Shape srcShape in srcPage.Shapes)
                {
                    // Skip deleted shapes
                    if (srcShape.Del == BOOL.True)
                        continue;

                    // Generate a unique file name for the shape PDF
                    string shapePdfPath = Path.Combine(
                        tempFolder,
                        $"Page{srcPage.ID}_Shape{srcShape.ID}.pdf");

                    // Export the shape to a vector PDF page
                    srcShape.ToPdf(shapePdfPath);

                    shapePdfFiles.Add(shapePdfPath);
                }
            }

            // Combine all shape PDFs into a single PDF document
            // Use fully qualified Aspose.Pdf types to avoid namespace conflicts
            Aspose.Pdf.Document finalPdf = new Aspose.Pdf.Document();

            foreach (string shapePdfPath in shapePdfFiles)
            {
                // Load the individual shape PDF
                Aspose.Pdf.Document shapeDoc = new Aspose.Pdf.Document(shapePdfPath);

                // Add its first (and only) page to the final document
                finalPdf.Pages.Add(shapeDoc.Pages[1]);
            }

            // Save the combined PDF
            string outputPdfPath = "AllShapes.pdf";
            finalPdf.Save(outputPdfPath);

            // Cleanup temporary files
            foreach (string file in shapePdfFiles)
            {
                try { File.Delete(file); } catch { /* ignore */ }
            }
            try { Directory.Delete(tempFolder, true); } catch { /* ignore */ }

            // Dispose diagrams
            sourceDiagram.Dispose();
            finalPdf.Dispose();

            Console.WriteLine($"Export completed. Combined PDF saved to: {outputPdfPath}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
