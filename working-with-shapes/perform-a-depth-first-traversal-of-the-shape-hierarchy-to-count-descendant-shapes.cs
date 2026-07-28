using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Example: count descendants of the first shape on the first page
            Shape rootShape = diagram.Pages[0].Shapes[0];
            int descendantCount = CountDescendants(rootShape);

            Console.WriteLine($"Descendant shapes count: {descendantCount}");

            // Save the diagram (no modifications made)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Performs a depth‑first traversal of the shape hierarchy
    // and returns the total number of descendant shapes.
    static int CountDescendants(Shape shape)
    {
        int count = 0;

        // Shape.Shapes contains child shapes (e.g., members of a group)
        foreach (Shape child in shape.Shapes)
        {
            // Count the child itself
            count++;

            // Recursively count the child's descendants
            count += CountDescendants(child);
        }

        return count;
    }
}
