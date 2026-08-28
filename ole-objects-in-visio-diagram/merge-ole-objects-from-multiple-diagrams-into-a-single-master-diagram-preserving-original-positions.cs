using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;

class OleObjectsMerger
{
    // Merges OLE objects from multiple Visio diagrams into a single master diagram.
    // The original positions of the OLE objects are preserved by using Diagram.Combine.
    public static void MergeDiagrams(string masterDiagramPath, IEnumerable<string> otherDiagramPaths, string outputPath)
    {
        // Load the master diagram (the base diagram that will receive the others)
        Diagram masterDiagram = new Diagram(masterDiagramPath);

        // Iterate through each additional diagram and combine it with the master
        foreach (string diagramPath in otherDiagramPaths)
        {
            // Load the current diagram to be merged
            Diagram secondDiagram = new Diagram(diagramPath);

            // Combine the second diagram into the master diagram.
            // This method merges pages, shapes (including OLE objects) and preserves their coordinates.
            masterDiagram.Combine(secondDiagram);

            // Dispose the second diagram as it is no longer needed
            secondDiagram.Dispose();
        }

        // Save the combined diagram to the specified output file in VDX format
        masterDiagram.Save(outputPath, SaveFileFormat.Vdx);

        // Clean up the master diagram
        masterDiagram.Dispose();
    }

    // Example usage
    static void Main()
    {
        try
        {

            // Path to the initial master diagram
            string masterPath = @"C:\Diagrams\Master.vdx";

            // Paths to other diagrams whose OLE objects should be merged
            List<string> otherPaths = new List<string>
            {
                @"C:\Diagrams\Diagram1.vdx",
                @"C:\Diagrams\Diagram2.vdx",
                @"C:\Diagrams\Diagram3.vdx"
            };

            // Output path for the merged diagram
            string outputPath = @"C:\Diagrams\MergedResult.vdx";

            // Perform the merge
            MergeDiagrams(masterPath, otherPaths, outputPath);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
