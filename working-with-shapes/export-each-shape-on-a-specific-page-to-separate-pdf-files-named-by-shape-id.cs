using System.IO;
using System;
using Aspose.Diagram;

class ExportShapesToPdf
{
    static void Main()
    {
        try
        {

            // Path to the source Visio diagram
            string sourceFile = "input.vsdx";

            // Index of the page to process (0‑based)
            int pageIndex = 0;

            // Load the diagram
            Diagram diagram = new Diagram(sourceFile);

            // Get the specified page
            Page page = diagram.Pages[pageIndex];

            // Export each shape on the page to a separate PDF file named by its ID
            foreach (Shape shape in page.Shapes)
            {
                // Build the output file name using the shape's ID
                string outputFile = $"Shape_{shape.ID}.pdf";

                // Save the shape as a PDF
                shape.ToPdf(outputFile);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
