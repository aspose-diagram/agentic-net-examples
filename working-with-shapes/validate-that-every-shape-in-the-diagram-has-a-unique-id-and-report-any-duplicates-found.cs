using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;

public class DiagramValidator
{
    /// <summary>
    /// Validates that every shape in the diagram has a unique ID.
    /// Prints duplicate IDs and the shapes that share them.
    /// </summary>
    /// <param name="filePath">Path to the Visio diagram file (e.g., .vsdx).</param>
    public static void ValidateUniqueShapeIds(string filePath)
    {
        // Load the diagram using Aspose.Diagram
        Diagram diagram = new Diagram(filePath);

        // Dictionary to map shape ID to list of shapes that use it
        var idMap = new Dictionary<long, List<Shape>>();

        // Iterate through all pages and their shapes
        foreach (Page page in diagram.Pages)
        {
            foreach (Shape shape in page.Shapes)
            {
                long shapeId = shape.ID;

                // Add shape to the map
                if (!idMap.ContainsKey(shapeId))
                {
                    idMap[shapeId] = new List<Shape>();
                }
                idMap[shapeId].Add(shape);
            }
        }

        // Find and report duplicate IDs
        bool duplicatesFound = false;
        foreach (var kvp in idMap)
        {
            if (kvp.Value.Count > 1)
            {
                duplicatesFound = true;
                Console.WriteLine($"Duplicate Shape ID: {kvp.Key}");
                foreach (Shape dupShape in kvp.Value)
                {
                    // Report page name and shape name for context
                    string pageName = dupShape.Page != null ? dupShape.Page.NameU : "UnknownPage";
                    string shapeName = dupShape.NameU ?? "UnnamedShape";
                    Console.WriteLine($"\tPage: {pageName}, Shape Name: {shapeName}");
                }
            }
        }

        if (!duplicatesFound)
        {
            Console.WriteLine("All shape IDs are unique.");
        }
    }
}

// Example usage:
// DiagramValidator.ValidateUniqueShapeIds("C:\\Diagrams\\sample.vsdx");

class Program
{
    static void Main(string[] args)
    {
        try
        {

            DiagramValidator.ValidateUniqueShapeIds("");

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
