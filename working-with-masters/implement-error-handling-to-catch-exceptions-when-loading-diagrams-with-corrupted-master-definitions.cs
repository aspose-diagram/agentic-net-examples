using System.IO;
using System;
using Aspose.Diagram;

class DiagramLoader
{
    // Attempts to load a Visio diagram and handles corrupted master definitions.
    public static Diagram LoadDiagram(string filePath)
    {
        try
        {
            // Load the diagram from the specified file.
            // This may throw DiagramException if the file contains corrupted masters.
            Diagram diagram = new Diagram(filePath);
            return diagram;
        }
        catch (DiagramException dex)
        {
            // Specific handling for Aspose.Diagram loading errors.
            Console.Error.WriteLine($"Diagram loading failed: {dex.Message}");
            return null;
        }
        catch (Exception ex)
        {
            // Fallback for any other unexpected errors.
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
            return null;
        }
    }

    static void Main()
    {
        string path = "sample.vsdx";

        Diagram diagram = LoadDiagram(path);
        if (diagram != null)
        {
            // Example usage: output number of masters in the loaded diagram.
            Console.WriteLine($"Diagram loaded successfully. Masters count: {diagram.Masters.Count}");
            diagram.Dispose();
        }
        else
        {
            Console.WriteLine("Failed to load diagram.");
        }
    }
}
