using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Get the first page (index 0) of the diagram
            Page firstPage = diagram.Pages[0];

            // Add a triangle shape to the first page at position (4, 5) inches
            // "Triangle" is the name of the built‑in master shape in Visio
            firstPage.AddShape(4.0, 5.0, "Triangle");

            // Save the diagram (using the provided Save method)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
