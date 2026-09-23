using System.IO;
using Aspose.Diagram;
using System;

class Program
{
    static void Main()
    {
        try
        {

            // Load the existing Visio diagram (preserves original format)
            Diagram diagram = new Diagram("input.vsdx");

            // TODO: Apply any required updates to the diagram here
            // Example: diagram.Pages[0].Name = "UpdatedPage";

            // Save the updated diagram to a new file, keeping the Visio format (VSDX)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
