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

            // Load the VSD diagram from file
            Diagram diagram = new Diagram("input.vsd");

            // Save the diagram as SVG using the SaveFileFormat.Svg enum
            diagram.Save("output.svg", SaveFileFormat.Svg);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
