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

            // Load the first diagram (VSD) preserving its page order
            Diagram firstDiagram = new Diagram("first.vsd", LoadFileFormat.Vsd);

            // Load the second diagram (VSDX)
            Diagram secondDiagram = new Diagram("second.vsdx", LoadFileFormat.Vsdx);

            // Combine the second diagram into the first one.
            // The Combine method appends pages from the second diagram after the pages of the first diagram,
            // thus preserving the original order of the first diagram's pages.
            firstDiagram.Combine(secondDiagram);

            // Save the combined diagram. Here we choose VSDX as the output format.
            firstDiagram.Save("combined.vsdx", SaveFileFormat.Vsdx);

            // Clean up resources
            firstDiagram.Dispose();
            secondDiagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
