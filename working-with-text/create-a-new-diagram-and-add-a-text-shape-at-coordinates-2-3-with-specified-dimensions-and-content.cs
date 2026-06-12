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

            // Create a new diagram instance
            Diagram diagram = new Diagram();

            // Access the active page of the diagram
            Page page = diagram.ActivePage;

            // Add a text shape at coordinates (2, 3) with width 2, height 1 and the desired text
            Shape textShape = page.AddText(2.0, 3.0, 2.0, 1.0, "Sample Text");

            // Save the diagram to a VDX file
            diagram.Save("output.vdx", SaveFileFormat.Vdx);

        }
        catch (System.NullReferenceException ex)
        {
            Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
        }
    }
}
