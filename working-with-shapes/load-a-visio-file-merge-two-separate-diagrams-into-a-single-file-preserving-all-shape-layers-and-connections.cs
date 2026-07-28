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

            // Load the first Visio diagram
            var diagram1 = new Diagram("FirstDiagram.vsdx");

            // Load the second Visio diagram
            var diagram2 = new Diagram("SecondDiagram.vsdx");

            // Merge the second diagram into the first.
            // Combine preserves all shape layers, connections, masters, etc.
            diagram1.Combine(diagram2);

            // Save the merged diagram to a new file
            diagram1.Save("MergedDiagram.vsdx", SaveFileFormat.Vsdx);

            // Clean up resources
            diagram1.Dispose();
            diagram2.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
