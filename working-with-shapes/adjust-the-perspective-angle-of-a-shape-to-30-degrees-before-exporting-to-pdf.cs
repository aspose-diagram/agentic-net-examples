using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Access a shape (example: first shape on the first page)
            Shape shape = diagram.Pages[0].Shapes[0];

            // Set the 3‑D perspective angle to 30 degrees
            shape.ThreeDFormat.Perspective.Value = 30;

            // Export the shape to a PDF file
            shape.ToPdf("output.pdf");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
