using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string sourceFile = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(sourceFile);

            // Identify the page and shape to export
            // Adjust page index and shape ID as needed
            int pageIndex = 0;               // first page
            long shapeId = 1;                // ID of the target shape

            // Retrieve the shape instance
            Shape targetShape = diagram.Pages[pageIndex].Shapes.GetShape(shapeId);

            // Define the output PDF file path
            string outputPdf = "exported_shape.pdf";

            // Export the shape to PDF.
            // For vector PDF the concept of DPI does not apply; the shape is saved at full vector quality.
            targetShape.ToPdf(outputPdf);

            Console.WriteLine("Shape has been exported to PDF: " + outputPdf);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
