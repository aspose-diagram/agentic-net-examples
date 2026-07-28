using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Get the first page (index 0) and a shape by its ID (example ID = 1)
            Shape shape = diagram.Pages[0].Shapes.GetShape(1);

            // Rotate the shape's text block by 45 degrees
            shape.TextXForm.TxtAngle.Value = 45.0;

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
