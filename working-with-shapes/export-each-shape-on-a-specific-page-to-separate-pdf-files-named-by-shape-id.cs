using System;
using System.IO;
using Aspose.Diagram;

class ExportShapesToPdf
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Index of the page to process (0‑based). Change as needed.
            int pageIndex = 0;

            // Retrieve the specific page
            Page page = diagram.Pages[pageIndex];

            // Ensure the output folder exists
            string outputFolder = "ShapePdfs";
            Directory.CreateDirectory(outputFolder);

            // Export each shape on the page to an individual PDF file named by its ID
            foreach (Shape shape in page.Shapes)
            {
                // Build the PDF file name using the shape's unique ID
                string pdfFilePath = Path.Combine(outputFolder, $"Shape_{shape.ID}.pdf");

                // Save the shape as a PDF
                shape.ToPdf(pdfFilePath);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
