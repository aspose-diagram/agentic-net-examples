using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the Visio diagram file as the first argument.");
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
            // Load the diagram inside a using block (mandatory pattern)
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Manual page counter because Page does not have an Index property
                int pageNumber = 0;

                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    pageNumber++;
                    Console.WriteLine($"Processing Page {pageNumber} (Name: {page.Name})...");

                    // Iterate through top‑level shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Count all descendant shapes using depth‑first traversal
                        int descendantCount = CountDescendants(shape);

                        Console.WriteLine($"Shape ID {shape.ID} (Name: {shape.Name}) has {descendantCount} descendant(s).");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    /// <summary>
    /// Recursively counts all descendant shapes of the given shape using depth‑first traversal.
    /// </summary>
    /// <param name="shape">The shape whose descendants are to be counted.</param>
    /// <returns>The total number of descendant shapes.</returns>
    private static int CountDescendants(Shape shape)
    {
        int count = 0;

        // Guard against null Shapes collection (non‑group shapes)
        if (shape.Shapes != null)
        {
            // Iterate through each child shape
            foreach (Shape child in shape.Shapes)
            {
                // Count the child itself
                count++;

                // Recursively count the child's descendants
                count += CountDescendants(child);
            }
        }

        return count;
    }
}