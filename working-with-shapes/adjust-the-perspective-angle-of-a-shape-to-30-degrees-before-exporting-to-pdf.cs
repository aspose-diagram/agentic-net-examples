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

            // Ensure there is at least one page and one shape
            if (diagram.Pages.Count > 0)
            {
                Page page = diagram.Pages[0];
                if (page.Shapes.Count > 0)
                {
                    // Get the first shape on the page
                    Shape shape = page.Shapes.GetShape(1);

                    // Set the perspective angle to 30 degrees
                    shape.ThreeDFormat.Perspective.Value = 30.0;

                    // Prepare PDF save options (optional: set default font)
                    PdfSaveOptions pdfOptions = new PdfSaveOptions();
                    pdfOptions.DefaultFont = "Arial";

                    // Export the diagram to PDF
                    diagram.Save("output.pdf", pdfOptions);
                    Console.WriteLine("Diagram exported to PDF with perspective angle set to 30 degrees.");
                }
                else
                {
                    Console.WriteLine("No shapes found on the first page.");
                }
            }
            else
            {
                Console.WriteLine("The diagram contains no pages.");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
