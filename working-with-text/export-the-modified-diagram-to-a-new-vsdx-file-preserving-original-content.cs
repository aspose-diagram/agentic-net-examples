using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the existing Visio diagram (VS DX format)
            Diagram diagram = new Diagram("input.vsdx");

            // -------------------------------------------------
            // Place any diagram modifications here.
            // Example (optional): rename the first page.
            // if (diagram.Pages.Count > 0)
            // {
            //     diagram.Pages[0].Name = "ModifiedPage";
            // }
            // -------------------------------------------------

            // Save the diagram to a new VS DX file, preserving all original content
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
