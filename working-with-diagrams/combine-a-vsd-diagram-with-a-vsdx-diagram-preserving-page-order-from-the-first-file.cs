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

            // Paths to the source diagrams
            string firstDiagramPath = "first.vsd";   // VSD file (first diagram)
            string secondDiagramPath = "second.vsdx"; // VSDX file (second diagram)

            // Path for the combined output diagram
            string outputDiagramPath = "combined.vsdx";

            // Load the first diagram (its page order will be preserved)
            Diagram firstDiagram = new Diagram(firstDiagramPath);

            // Load the second diagram
            Diagram secondDiagram = new Diagram(secondDiagramPath);

            // Combine the second diagram into the first one.
            // Pages from the second diagram are appended after the pages of the first diagram,
            // thus preserving the original order of the first diagram's pages.
            firstDiagram.Combine(secondDiagram);

            // Save the combined diagram as VSDX
            firstDiagram.Save(outputDiagramPath, SaveFileFormat.Vsdx);

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
