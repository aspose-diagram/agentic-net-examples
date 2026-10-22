using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the Visio file as the first argument.");
            return;
        }

        string diagramPath = args[0];

        // Guard against missing file
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (mandatory)
            using (Diagram diagram = new Diagram(diagramPath))
            {
                int totalDescendants = 0;

                // Iterate through each page in the diagram
                for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
                {
                    Page page = diagram.Pages[pageIndex];

                    // Iterate through top‑level shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Count all descendant shapes of the current shape using depth‑first traversal
                        int descendantCount = CountDescendants(shape);
                        totalDescendants += descendantCount;
                    }
                }

                // Output the result
                Console.WriteLine($"Total number of descendant shapes (depth‑first): {totalDescendants}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    /// <summary>
    /// Recursively counts all descendant shapes of the given shape using depth‑first traversal.
    /// </summary>
    /// <param name="shape">The parent shape.</param>
    /// <returns>The number of descendant shapes.</returns>
    private static int CountDescendants(Shape shape)
    {
        int count = 0;

        // Group shapes contain child shapes in the Shapes collection.
        // For non‑group shapes there are no descendants.
        if (shape.Type == TypeValue.Group && shape.Shapes != null)
        {
            foreach (Shape child in shape.Shapes)
            {
                // Count the child itself
                count += 1;

                // Recursively count the child's descendants
                count += CountDescendants(child);
            }
        }

        return count;
    }
}