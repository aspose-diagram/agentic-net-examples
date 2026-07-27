using System.IO;
using System;
using Aspose.Diagram;

class DiagramValidator
{
    // Validates that the diagram at the given path contains at least one shape.
    public static void Validate(string filePath)
    {
        // Load the diagram using the provided constructor (lifecycle rule).
        Diagram diagram = new Diagram(filePath);

        // Check each page for shapes.
        bool hasShape = false;
        foreach (Page page in diagram.Pages)
        {
            if (page.Shapes.Count > 0)
            {
                hasShape = true;
                break;
            }
        }

        // Throw if no shapes are found.
        if (!hasShape)
        {
            throw new InvalidOperationException("The diagram does not contain any shapes.");
        }

        // Proceed with further processing here.
    }
}

class Program
{
    static void Main(string[] args)
    {
        try
        {

            DiagramValidator.Validate("");

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
