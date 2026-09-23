using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Example relative path to a VSD diagram
            string relativePath = @"Diagrams\SampleDiagram.vsd";

            // Resolve the relative path to an absolute path
            string absolutePath = Path.GetFullPath(relativePath);

            // Load the diagram using the absolute path
            Diagram diagram = new Diagram(absolutePath);

            // Perform any required operations on the diagram here
            // ...

            // Example: save the diagram to a new file (optional)
            string outputPath = Path.Combine(Path.GetDirectoryName(absolutePath), "SampleDiagram_Processed.vsd");
            diagram.Save(outputPath, SaveFileFormat.Vsd);

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
