using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the VSD diagram from file
            Diagram diagram = new Diagram("input.vsd");

            // Save the diagram as SVG using the Save method with SaveFileFormat.Svg
            diagram.Save("output.svg", SaveFileFormat.Svg);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
