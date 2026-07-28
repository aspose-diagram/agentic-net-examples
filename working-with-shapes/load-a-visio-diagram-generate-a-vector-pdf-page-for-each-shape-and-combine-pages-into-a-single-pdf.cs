using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Input Visio file (change the path as needed)
            string inputPath = "input.vsdx";

            // Output combined PDF file
            string outputPdf = "CombinedShapes.pdf";

            // Temporary folder to store individual shape PDFs
            string tempFolder = Path.Combine(Path.GetTempPath(), "ShapePdfs_" + Guid.NewGuid().ToString());
            Directory.CreateDirectory(tempFolder);

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            int shapeIndex = 0;

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    // Export each shape to a separate PDF file
                    string shapePdfPath = Path.Combine(tempFolder, $"shape_{shapeIndex}.pdf");
                    shape.ToPdf(shapePdfPath);
                    shapeIndex++;
                }
            }

            // Combine all individual PDFs into a single PDF using Aspose.Pdf (fully qualified)
            Aspose.Pdf.Document finalDoc = new Aspose.Pdf.Document();

            foreach (string pdfFile in Directory.GetFiles(tempFolder, "*.pdf"))
            {
                Aspose.Pdf.Document tempDoc = new Aspose.Pdf.Document(pdfFile);
                foreach (Aspose.Pdf.Page page in tempDoc.Pages)
                {
                    finalDoc.Pages.Add(page);
                }
            }

            // Save the combined PDF
            finalDoc.Save(outputPdf);

            // Clean up temporary files
            Directory.Delete(tempFolder, true);

            Console.WriteLine($"Combined PDF saved to: {outputPdf}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
