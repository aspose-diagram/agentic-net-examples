using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file to be loaded
            string filePath = @"C:\Diagrams\sample.vsdx";

            // Load the Visio diagram and obtain the Diagram object
            Diagram diagram = new Diagram(filePath);

            // The 'diagram' object can now be used for further processing

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
