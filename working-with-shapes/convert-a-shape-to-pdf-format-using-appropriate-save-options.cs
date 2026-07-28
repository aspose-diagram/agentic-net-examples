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

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Access the first page (adjust index as needed)
            Page page = diagram.Pages[0];

            // Retrieve a shape from the page (e.g., shape with ID 1)
            Shape shape = page.Shapes.GetShape(1);

            // Destination PDF file for the shape
            string pdfFile = "shape.pdf";

            // Convert the shape to PDF using the built‑in ToPdf method
            shape.ToPdf(pdfFile);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
