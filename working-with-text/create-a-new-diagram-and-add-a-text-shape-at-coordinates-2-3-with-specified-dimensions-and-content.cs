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

            // Access the active page of the diagram
            Page page = diagram.ActivePage;

            // Add a text shape at coordinates (2,3) with width 2, height 1 and the desired text
            Shape textShape = page.AddText(2, 3, 2, 1, "Your text content here");

            // Refresh shape data to ensure proper rendering
            textShape.RefreshData();

            // Save the diagram to a VSDX file
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.NullReferenceException ex)
        {
            Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
        }
    }
}
