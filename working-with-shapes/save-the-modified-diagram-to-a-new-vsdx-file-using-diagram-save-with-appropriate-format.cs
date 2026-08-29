using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your source file)
            Diagram diagram = new Diagram("input.vsdx");

            // ---- Perform any modifications to the diagram here ----
            // Example: change the title of the document (optional)
            // diagram.DocumentProps.Title = "Modified Diagram";

            // Save the modified diagram to a new VSDX file using the Save method
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
