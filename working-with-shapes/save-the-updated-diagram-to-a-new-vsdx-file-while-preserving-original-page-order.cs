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

            // Load the original diagram (replace with your source file path)
            Diagram diagram = new Diagram("input.vsdx");

            // TODO: Apply any modifications to the diagram here

            // Save the updated diagram to a new VSDX file.
            // The Save method preserves the original page order.
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
