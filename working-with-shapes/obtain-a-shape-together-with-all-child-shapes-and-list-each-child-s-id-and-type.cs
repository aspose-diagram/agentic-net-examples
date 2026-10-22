using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <shapeName>");
            return;
        }

        string diagramPath = args[0];
        string targetShapeName = args[1];

        // Guard against missing diagram file.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (required pattern).
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Locate the shape by name, searching across all pages and including nested group children.
                Shape targetShape = null;
                foreach (Page page in diagram.Pages)
                {
                    // GetShapeIncludingChild searches within the page's shape collection and its groups.
                    targetShape = page.Shapes.GetShapeIncludingChild(targetShapeName);
                    if (targetShape != null)
                    {
                        break;
                    }
                }

                if (targetShape == null)
                {
                    Console.Error.WriteLine($"Error: Shape with name '{targetShapeName}' not found.");
                    return;
                }

                // Output basic information about the found shape.
                Console.WriteLine($"Found shape: ID = {targetShape.ID}, Name = {targetShape.NameU}, Type = {targetShape.Type}");

                // List child shapes if the shape is a group (has a Shapes collection).
                // The Shapes collection is null for non‑group shapes.
                if (targetShape.Shapes != null && targetShape.Shapes.Count > 0)
                {
                    Console.WriteLine("Child shapes:");
                    foreach (Shape child in targetShape.Shapes)
                    {
                        // Output each child's ID and Type.
                        Console.WriteLine($"  Child ID: {child.ID}, Type: {child.Type}");
                    }
                }
                else
                {
                    Console.WriteLine("The specified shape has no child shapes.");
                }
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}