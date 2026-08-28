using System.IO;
using System;
using Aspose.Diagram;

public class DiagramLoader
{
    /// <summary>
    /// Loads a Visio diagram from the specified file path.
    /// Handles exceptions that occur when the diagram contains corrupted master definitions.
    /// </summary>
    /// <param name="filePath">Full path to the Visio file.</param>
    /// <returns>The loaded Diagram object, or null if loading failed.</returns>
    public Diagram LoadDiagram(string filePath)
    {
        Diagram diagram = null;

        try
        {
            // Load the diagram using the provided constructor (lifecycle rule)
            diagram = new Diagram(filePath);
        }
        catch (DiagramException dex)
        {
            // Catch Aspose.Diagram specific exceptions (e.g., corrupted master definitions)
            Console.WriteLine($"Diagram loading failed: {dex.Message}");
            // Additional handling logic can be placed here (logging, fallback, etc.)
        }
        catch (Exception ex)
        {
            // Catch any other unexpected exceptions
            Console.WriteLine($"Unexpected error: {ex.Message}");
        }

        return diagram;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // See classes above
    }
}
