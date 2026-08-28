using System.IO;
using Aspose.Diagram;
using System;

class Program
{
    static void Main()
    {
        try
        {

            // Load the existing Visio file
            Diagram diagram = new Diagram("input.vsdx");

            // Insert a pentagon shape at coordinates (5, 5) inches on the active page
            diagram.ActivePage.AddShape(5.0, 5.0, "Pentagon");

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
