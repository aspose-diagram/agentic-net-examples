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

            // Load the Visio diagram from a file
            Diagram diagram = new Diagram("input.vsdx");

            // Save the entire diagram as an SVG file.
            // This preserves the shape hierarchy and all style information.
            diagram.Save("output.svg", SaveFileFormat.Svg);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
