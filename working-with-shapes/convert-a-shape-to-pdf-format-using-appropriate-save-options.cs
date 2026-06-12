using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram from a file
            Diagram diagram = new Diagram("input.vsdx");

            // Access a specific shape (e.g., the first shape on the first page)
            Page page = diagram.Pages[0];
            Shape shape = page.Shapes[0];

            // Convert the selected shape to PDF and save it to a file
            shape.ToPdf("shape.pdf");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
