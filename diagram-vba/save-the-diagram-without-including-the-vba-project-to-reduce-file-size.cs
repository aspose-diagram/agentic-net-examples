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

            // Load the existing Visio diagram from a file
            Diagram diagram = new Diagram("input.vsdx");

            // Remove any VBA/macros from the diagram to reduce file size
            diagram.RemoveMacro();

            // Save the diagram back to a file (same format as the original)
            // Using SaveFileFormat to specify the output format
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            // Alternatively, you can use SaveOptions if you need more control:
            // DiagramSaveOptions options = new DiagramSaveOptions(SaveFileFormat.Vsdx);
            // diagram.Save("output.vsdx", options);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
