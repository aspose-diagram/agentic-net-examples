using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load the existing Visio diagram (any supported format)
            var diagram = new Diagram("input.vsdx");

            // Perform any required modifications to the diagram here
            // ...

            // Save the updated diagram to a new VSDX file.
            // The Save method with SaveFileFormat.Vsdx preserves the original page order.
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
