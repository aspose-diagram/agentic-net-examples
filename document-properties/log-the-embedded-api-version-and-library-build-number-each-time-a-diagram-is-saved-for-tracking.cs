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

            // Load the diagram from a file
            Diagram diagram = new Diagram("input.vsdx");

            // Log the Visio version and build number embedded in the diagram
            Console.WriteLine($"Visio Version: {diagram.Version}");
            Console.WriteLine($"Visio Build Number: {diagram.Buildnum}");

            // Save the diagram using Aspose.Diagram's Save method
            DiagramSaveOptions saveOptions = new DiagramSaveOptions(SaveFileFormat.Vdx);
            diagram.Save("output.vdx", saveOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
