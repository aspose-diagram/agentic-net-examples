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

            // Load the first VSDX diagram.
            Diagram diagram1 = new Diagram("FirstDiagram.vsdx");

            // Load the second VSDX diagram.
            Diagram diagram2 = new Diagram("SecondDiagram.vsdx");

            // Merge the second diagram into the first one.
            // Combine automatically resolves duplicate shape IDs.
            diagram1.Combine(diagram2);

            // Save the merged diagram to a new file.
            diagram1.Save("MergedDiagram.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
