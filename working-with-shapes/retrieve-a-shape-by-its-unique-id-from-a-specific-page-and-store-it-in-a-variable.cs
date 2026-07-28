using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Load the Visio diagram (replace the path with your file)
            Aspose.Diagram.Diagram diagram = new Aspose.Diagram.Diagram("input.vsdx");

            // Define the page index (zero‑based) and the unique shape ID you want to retrieve
            int pageIndex = 0;          // e.g., first page
            long shapeId = 12345;       // the unique ID of the shape on that page

            // Get the specific page from the diagram
            Aspose.Diagram.Page page = diagram.Pages.GetPage(pageIndex);

            // Retrieve the shape by its unique ID from the page's shape collection
            Aspose.Diagram.Shape targetShape = page.Shapes.GetShape(shapeId);

            // Now 'targetShape' holds the reference to the requested shape and can be used further.

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
