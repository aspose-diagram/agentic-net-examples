using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Path to the source Visio file
            string visioPath = "input.vsdx";
            // Path where the shape will be saved as PDF
            string pdfPath = "shape.pdf";

            // Load the diagram from file
            Diagram diagram = new Diagram(visioPath);

            // Access the first page of the diagram
            Page page = diagram.Pages[0];

            // Locate the first shape that is not marked as deleted
            Shape targetShape = null;
            foreach (Shape shape in page.Shapes)
            {
                if (shape.Del == BOOL.False)
                {
                    targetShape = shape;
                    break;
                }
            }

            if (targetShape == null)
            {
                Console.WriteLine("No suitable shape found to export.");
                return;
            }

            // Export the selected shape to a PDF file
            targetShape.ToPdf(pdfPath);
            Console.WriteLine($"Shape successfully exported to PDF: {pdfPath}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
