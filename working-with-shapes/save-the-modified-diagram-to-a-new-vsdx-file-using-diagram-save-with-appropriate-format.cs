using System.IO;
using Aspose.Diagram;
using System;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your source file)
            Diagram diagram = new Diagram("input.vsdx");

            // TODO: Apply any modifications to the diagram here
            // Example: diagram.Pages[0].Shapes[0].Text.Value = "Modified";

            // Save the modified diagram as a new VSDX file
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
