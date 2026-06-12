using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    // Recursively counts all descendant shapes of a given shape (including nested groups)
    static int CountDescendants(Shape shape)
    {
        int count = 0;

        // Group shapes contain their own Shapes collection
        if (shape.Shapes != null && shape.Shapes.Count > 0)
        {
            foreach (Shape child in shape.Shapes)
            {
                // Count the direct child
                count++;

                // Add the child's own descendants
                count += CountDescendants(child);
            }
        }

        return count;
    }

    static void Main()
    {
        try
        {

            // Load a Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            int totalDescendants = 0;

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through top‑level shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    int descendantCount = CountDescendants(shape);
                    totalDescendants += descendantCount;

                    Console.WriteLine($"Shape ID {shape.ID} has {descendantCount} descendant shape(s).");
                }
            }

            Console.WriteLine($"Total descendant shapes in the diagram: {totalDescendants}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
