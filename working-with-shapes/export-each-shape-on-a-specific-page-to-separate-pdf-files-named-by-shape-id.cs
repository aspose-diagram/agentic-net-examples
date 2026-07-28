using System;
using System.IO;
using Aspose.Diagram;

class ExportShapesToPdf
{
    static void Main()
    {
        try
        {

            // Path to the source Visio diagram
            string sourceFile = @"C:\Diagrams\sample.vsdx";

            // Folder where individual shape PDFs will be saved
            string outputFolder = @"C:\Diagrams\ShapePdfs";

            // Index of the page to process (0‑based). Change as needed.
            int pageIndex = 0;

            // Ensure the output directory exists
            Directory.CreateDirectory(outputFolder);

            // Load the Visio diagram
            Diagram diagram = new Diagram(sourceFile);

            // Validate page index
            if (pageIndex < 0 || pageIndex >= diagram.Pages.Count)
            {
                Console.WriteLine("Invalid page index.");
                return;
            }

            // Get the specific page
            Page page = diagram.Pages[pageIndex];

            // Iterate through each shape on the page
            foreach (Shape shape in page.Shapes)
            {
                // Shape ID is unique within the page
                long shapeId = shape.ID;

                // Build the PDF file name using the shape ID
                string pdfPath = Path.Combine(outputFolder, $"Shape_{shapeId}.pdf");

                // Export the shape to a PDF file
                shape.ToPdf(pdfPath);
            }

            Console.WriteLine("Export completed.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
