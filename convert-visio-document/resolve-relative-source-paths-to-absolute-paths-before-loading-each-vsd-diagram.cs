using System;
using System.IO;
using Aspose.Diagram;

class DiagramLoader
{
    // Loads a Visio diagram from a relative path by converting it to an absolute path first.
    public static Diagram LoadDiagram(string relativePath)
    {
        // Resolve the relative path to an absolute path based on the current working directory.
        string absolutePath = Path.GetFullPath(relativePath);

        // Use the Aspose.Diagram constructor that accepts a file name to load the diagram.
        Diagram diagram = new Diagram(absolutePath);

        return diagram;
    }

    // Example usage.
    static void Main()
    {
        try
        {

            // Relative path to the VSD file.
            string relativeDiagramPath = @"Resources\SampleDiagram.vsd";

            // Load the diagram with absolute path resolution.
            using (Diagram diagram = LoadDiagram(relativeDiagramPath))
            {
                // Perform operations on the loaded diagram here.
                Console.WriteLine($"Diagram loaded. Pages count: {diagram.Pages.Count}");
            }

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
