using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Map each Shape ID to the list of (Page, Shape) pairs that share that ID
            var idMap = new Dictionary<long, List<(Page page, Shape shape)>>();

            // Traverse all pages and their shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    long id = shape.ID;

                    if (!idMap.ContainsKey(id))
                    {
                        idMap[id] = new List<(Page, Shape)>();
                    }

                    idMap[id].Add((page, shape));
                }
            }

            // Detect and report duplicate IDs
            bool duplicatesFound = false;

            foreach (var entry in idMap)
            {
                if (entry.Value.Count > 1) // More than one shape with the same ID
                {
                    duplicatesFound = true;
                    Console.WriteLine($"Duplicate Shape ID: {entry.Key}");

                    foreach (var (page, shape) in entry.Value)
                    {
                        // Shape.Name may be empty; include page name for context
                        Console.WriteLine($"\tPage: {page.Name}, Shape Name: {shape.Name}");
                    }
                }
            }

            if (!duplicatesFound)
            {
                Console.WriteLine("No duplicate shape IDs found.");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
