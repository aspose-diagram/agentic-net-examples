using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class ExportDiagramWithTriangle
{
    static void Main()
    {
        try
        {

            // Load the existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // -----------------------------------------------------------------
            // 1. Export the whole diagram to a PDF file
            // -----------------------------------------------------------------
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            // Keep all pages together; no need to split
            pdfOptions.SplitMultiPages = false;
            diagram.Save("fullDiagram.pdf", pdfOptions);

            // -----------------------------------------------------------------
            // 2. Locate the triangle shape in the diagram
            // -----------------------------------------------------------------
            Shape triangleShape = null;
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Assuming the triangle shape is named "Triangle"
                    if (shape.NameU != null && shape.NameU.Equals("Triangle", StringComparison.OrdinalIgnoreCase))
                    {
                        triangleShape = shape;
                        break;
                    }
                }
                if (triangleShape != null) break;
            }

            // -----------------------------------------------------------------
            // 3. Export the triangle shape to a separate PDF page
            // -----------------------------------------------------------------
            if (triangleShape != null)
            {
                // This creates a PDF containing only the triangle shape.
                // The resulting file can be merged with the full diagram PDF
                // using any PDF manipulation library if a single document is required.
                triangleShape.ToPdf("triangleOnly.pdf");
            }
            else
            {
                Console.WriteLine("Triangle shape not found in the diagram.");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
