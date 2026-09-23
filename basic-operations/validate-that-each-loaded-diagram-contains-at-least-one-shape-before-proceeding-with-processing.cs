using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;

public class DiagramValidator
{
    // Validates that each diagram file contains at least one shape.
    // Throws InvalidOperationException if a diagram has no shapes.
    public static void ValidateDiagrams(IEnumerable<string> diagramFiles)
    {
        foreach (var filePath in diagramFiles)
        {
            // Load the diagram from the specified file.
            Diagram diagram = new Diagram(filePath);

            // Check each page for at least one shape.
            bool hasShape = false;
            foreach (Page page in diagram.Pages)
            {
                if (page.Shapes.Count > 0)
                {
                    hasShape = true;
                    break;
                }
            }

            if (!hasShape)
            {
                throw new InvalidOperationException(
                    $"The diagram \"{filePath}\" does not contain any shapes.");
            }

            // Continue processing the diagram here...
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        try
        {

            DiagramValidator.ValidateDiagrams(null);

        }
        catch (System.NullReferenceException ex)
        {
            Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
        }
    }
}
