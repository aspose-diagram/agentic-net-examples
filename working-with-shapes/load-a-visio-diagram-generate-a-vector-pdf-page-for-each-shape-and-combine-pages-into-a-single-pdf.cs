using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

public class Program
{
    public static void Main()
    {
        try
        {

            // Input Visio file path (adjust as needed)
            string inputPath = "input.vsdx";

            // Output combined PDF file path
            string outputPdfPath = "CombinedShapes.pdf";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Create a temporary directory to store individual shape PDFs
            string tempDir = Path.Combine(Path.GetTempPath(), "ShapePdfs_" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(tempDir);

            // List to keep track of generated PDF file paths
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
                    string shapePdfPath = Path.Combine(tempDir, $"Page{page.ID}_Shape{shape.ID}.pdf");

                    // Export the shape to a PDF file
                    shape.ToPdf(shapePdfPath);

                    // Store the path for later combination
                    shapePdfFiles.Add(shapePdfPath);
                }
            }

            // Combine all individual shape PDFs into a single PDF using Aspose.Pdf (fully qualified)
            Aspose.Pdf.Document combinedDoc = new Aspose.Pdf.Document();

            foreach (string pdfFile in shapePdfFiles)
            {
                // Load the individual PDF
                Aspose.Pdf.Document tempDoc = new Aspose.Pdf.Document(pdfFile);

                // Add its first page to the combined document
                combinedDoc.Pages.Add(tempDoc.Pages[1]);
            }

            // Save the combined PDF
            combinedDoc.Save(outputPdfPath);

            // Clean up temporary files and directory
            foreach (string pdfFile in shapePdfFiles)
            {
                try { File.Delete(pdfFile); } catch { }
            }

            try { Directory.Delete(tempDir, true); } catch { }

            Console.WriteLine($"Combined PDF saved to: {outputPdfPath}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
