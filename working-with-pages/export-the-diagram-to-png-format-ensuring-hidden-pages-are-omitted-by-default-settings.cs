using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (replace with your source file)
            Diagram diagram = new Diagram("input.vsdx");

            // Export the diagram to PNG.
            // By default Aspose.Diagram omits hidden pages during export.
            diagram.Save("output.png", SaveFileFormat.Png);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
