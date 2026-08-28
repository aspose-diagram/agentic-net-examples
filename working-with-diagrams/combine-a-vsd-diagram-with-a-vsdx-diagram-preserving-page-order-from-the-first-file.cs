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

            // Load the first diagram (VSD) – this diagram's page order will be kept first
            Diagram firstDiagram = new Diagram("first.vsd", LoadFileFormat.Vsd);

            // Load the second diagram (VSDX) – its pages will be appended after the first diagram's pages
            Diagram secondDiagram = new Diagram("second.vsdx", LoadFileFormat.Vsdx);

            // Combine the second diagram into the first one
            firstDiagram.Combine(secondDiagram);

            // Save the combined diagram preserving the VSDX format
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
