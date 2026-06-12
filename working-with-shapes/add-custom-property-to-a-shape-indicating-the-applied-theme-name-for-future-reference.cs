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

            // Access a specific shape (e.g., first shape on the first page)
            Shape shape = diagram.Pages[0].Shapes[1];

            // Store the applied theme name in a custom property (using Data1)
            shape.Data1 = "MyCustomTheme";

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
