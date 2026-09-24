using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Ensure a file path argument is provided.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: <program> <diagram-file-path>");
            return;
        }

        // Assign the diagram file path and verify its existence.
        string diagramPath = args[0];
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified file.
            Diagram diagram = new Diagram(diagramPath);

            // Iterate through each page in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through each shape on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Compute the number of descendant shapes using depth‑first recursion.
                    int descendantCount = CountDescendants(shape);

                    // Output the shape ID, its universal name, and the descendant count.
                    Console.WriteLine($"Shape ID {shape.ID} (NameU: {shape.NameU}) has {descendantCount} descendant shape(s).");
                }
            }
        }
        catch (Exception ex)
        {
            // Write any unexpected errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    // Recursively counts all descendant shapes of the given shape.
    private static int CountDescendants(Shape shape)
    {
        // If the shape is not a group, it has no descendants.
        if (shape.Type != TypeValue.Group)
            return 0;

        int count = 0;

        // Iterate through each child shape within the group.
        foreach (Shape child in shape.Shapes)
        {
            // Count the child itself.
            count += 1;

            // Add the child's own descendants (depth‑first).
            count += CountDescendants(child);
        }

        return count;
    }
}