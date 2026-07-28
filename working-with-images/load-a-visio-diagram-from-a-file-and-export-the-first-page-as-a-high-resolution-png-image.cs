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

            // Load the Visio diagram from a file (first page becomes the active page)
            Diagram diagram = new Diagram("input.vsdx");

            // Export the active (first) page as a high‑resolution PNG image
            // SaveFileFormat.Png tells Aspose.Diagram to render the page as PNG
            diagram.Save("firstPage.png", SaveFileFormat.Png);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
