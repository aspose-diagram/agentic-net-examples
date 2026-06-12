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

            // Load the first Visio diagram
            Diagram firstDiagram = new Diagram("Diagram1.vsdx");

            // Load the second Visio diagram
            Diagram secondDiagram = new Diagram("Diagram2.vsdx");

            // Merge the second diagram into the first one.
            // This preserves all shape layers, connections, and other document data.
            firstDiagram.Combine(secondDiagram);

            // Save the combined diagram to a new file.
            firstDiagram.Save("MergedDiagram.vsdx", SaveFileFormat.Vsdx);

            // Release resources
            firstDiagram.Dispose();
            secondDiagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
