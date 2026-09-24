using System.IO;
using Aspose.Diagram;
using System;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram from a VSDX file
            Diagram diagram = new Diagram("sample.vsdx");

            // Example: output number of pages to verify loading
            Console.WriteLine($"Diagram loaded successfully. Pages count: {diagram.Pages.Count}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
