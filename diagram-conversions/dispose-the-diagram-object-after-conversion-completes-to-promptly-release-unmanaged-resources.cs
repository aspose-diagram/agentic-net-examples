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

            // Load the source Visio diagram using the constructor that accepts a file path.
            Diagram diagram = new Diagram("input.vsdx");

            // Convert and save the diagram to another format (e.g., PDF) using the provided Save method.
            diagram.Save("output.pdf", SaveFileFormat.Pdf);

            // Release unmanaged resources promptly.
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
