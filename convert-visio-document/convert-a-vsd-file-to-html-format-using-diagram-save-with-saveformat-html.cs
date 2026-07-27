using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram from a VSD file
            Diagram diagram = new Diagram("input.vsd");

            // Save the diagram as HTML using the Save method with SaveFileFormat.Html
            diagram.Save("output.html", SaveFileFormat.Html);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
