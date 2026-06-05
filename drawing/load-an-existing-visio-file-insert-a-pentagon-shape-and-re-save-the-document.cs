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

            // Load the existing Visio file (replace with your actual file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Get the active page where the shape will be inserted
            Page page = diagram.ActivePage;

            // Insert a pentagon shape at coordinates (5, 5) inches
            // "Pentagon" is the master name for the pentagon shape in the default stencil
            page.AddShape(5.0, 5.0, "Pentagon");

            // Save the modified diagram to a new file
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
