using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    // Recursively counts all descendant shapes of the given shape using depth‑first traversal.
    static int CountDescendants(Shape shape)
    {
        int count = 0;

        // Iterate through direct child shapes.
        foreach (Shape child in shape.Shapes)
        {
            // Count the child itself.
            count++;

            // Recursively count the child's descendants.
            count += CountDescendants(child);
        }

        return count;
    }

    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with actual file path).
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate over each page in the diagram.
            foreach (Page page in diagram.Pages)
            {
                Console.WriteLine($"Page: {page.Name}");

                // Iterate over top‑level shapes on the page.
                foreach (Shape shape in page.Shapes)
                {
                    // Count all descendants of the current shape.
                    int descendantCount = CountDescendants(shape);

                    Console.WriteLine($"Shape ID {shape.ID} ('{shape.Name}') has {descendantCount} descendant shape(s).");
                }
            }

            // Save the diagram if any modifications were made (optional).
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
