using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the existing Visio diagram (auto-spaced)
            Diagram diagram = new Diagram("AutoSpacedDiagram.vsdx");

            // Export the diagram to PDF for sharing with non‑Visio users
            diagram.Save("AutoSpacedDiagram.pdf", SaveFileFormat.Pdf);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
