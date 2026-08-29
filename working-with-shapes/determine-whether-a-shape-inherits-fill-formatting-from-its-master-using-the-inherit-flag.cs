using System.IO;
using Aspose.Diagram;
using System;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Access the first page
            Page page = diagram.Pages[0];

            // Get a shape (skip the page shape at index 0)
            Shape shape = page.Shapes[1];

            // Determine if the shape inherits fill formatting from its master.
            // If FillStyle is null, the shape uses the master’s fill settings.
            bool inheritsFromMaster = shape.FillStyle == null;

            // Output the result
            Console.WriteLine($"Shape ID {shape.ID} inherits fill from master: {inheritsFromMaster}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
