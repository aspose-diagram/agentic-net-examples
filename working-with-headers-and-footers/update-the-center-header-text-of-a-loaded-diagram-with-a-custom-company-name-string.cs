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

            // Load an existing Visio diagram from file
            Diagram diagram = new Diagram("input.vsdx");

            // Update the center portion of the document's header with a custom company name
            diagram.HeaderFooter.HeaderCenter = "Acme Corporation";

            // Save the modified diagram back to disk
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
