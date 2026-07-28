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

            // Access the first page
            Page page = diagram.Pages[0];

            // Retrieve the first shape on the page
            // Get the shape's ID first, then obtain the shape via GetShape
            long firstShapeId = page.Shapes[0].ID;
            Shape shape = page.Shapes.GetShape(firstShapeId);

            // Set the fill pattern to solid (pattern index 1)
            shape.Fill.FillPattern.Value = 1;

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
