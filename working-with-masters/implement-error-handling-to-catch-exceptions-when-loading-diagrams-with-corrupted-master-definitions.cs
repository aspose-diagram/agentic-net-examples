using System.IO;
using System;
using Aspose.Diagram;

public class DiagramLoader
{
    // Loads a Visio diagram from the specified file path.
    // Returns the Diagram object if successful; otherwise returns null.
    public Diagram LoadDiagram(string filePath)
    {
        try
        {
            // Use the Diagram constructor that loads from a file.
            Diagram diagram = new Diagram(filePath);
            return diagram;
        }
        catch (DiagramException dex)
        {
            // Catches errors related to corrupted master definitions or other diagram issues.
            Console.Error.WriteLine($"Diagram loading failed: {dex.Message}");
            return null;
        }
        catch (Exception ex)
        {
            // Catches any other unexpected exceptions.
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
            return null;
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // See classes above
    }
}
