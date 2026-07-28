using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram from a file.
            // The Diagram(string) constructor loads the diagram automatically.
            Diagram diagram = new Diagram("sample.vsdx");

            // The 'diagram' variable now holds the loaded Diagram object
            // and can be used for further processing.
            Console.WriteLine("Diagram loaded successfully. Pages count: " + diagram.Pages.Count);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
