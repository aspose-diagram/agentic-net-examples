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

            // Load the Visio diagram from a file
            var diagram = new Diagram("input.vsdx");

            // Export the first page as a high‑resolution PNG image.
            // When saving as an image, Aspose.Diagram renders the active (first) page by default.
            diagram.Save("first_page.png", SaveFileFormat.Png);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
