using System.IO;
using System;
using Aspose.Diagram;

class ExportShapeToPdf
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram from file
            Diagram diagram = new Diagram("input.vsdx");

            // Identify the shape to export (example: shape with ID 5 on the first page)
            int shapeId = 5;
            Shape shape = diagram.Pages[0].Shapes.GetShape(shapeId);

            // Export the selected shape to a high‑resolution PDF file
            // Note: Shape.ToPdf does not expose resolution settings directly;
            // the method renders the shape at the best available quality.
            string outputPdfPath = "shape_output.pdf";
            shape.ToPdf(outputPdfPath);

            Console.WriteLine($"Shape {shapeId} exported to PDF: {outputPdfPath}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
