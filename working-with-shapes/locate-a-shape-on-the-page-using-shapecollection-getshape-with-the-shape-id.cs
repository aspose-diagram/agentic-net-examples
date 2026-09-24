using System.IO;
using Aspose.Diagram;
using System;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Specify the page index (0‑based) and the shape ID you want to locate
            int pageIndex = 0;          // first page
            long shapeId = 5;           // example shape ID

            // Retrieve the page from the diagram
            Page page = diagram.Pages[pageIndex];

            // Locate the shape on the page using its ID
            Shape shape = page.Shapes.GetShape(shapeId);

            // Example: output some shape information
            Console.WriteLine($"Shape ID: {shape.ID}, Name: {shape.Name}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
