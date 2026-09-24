using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: diagram file path and parent shape ID.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: <program> <diagramPath> <parentShapeId>");
            return;
        }

        // Assign and guard the diagram file path.
        string diagramPath = args[0];
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        // Parse and guard the parent shape ID.
        if (!long.TryParse(args[1], out long parentShapeId))
        {
            Console.Error.WriteLine($"Invalid parent shape ID: {args[1]}");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified file.
            Diagram diagram = new Diagram(diagramPath);

            // Prepare a list to collect child shape IDs.
            List<long> childShapeIds = new List<long>();

            // Iterate through all pages in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Retrieve the parent shape of the current shape.
                    Shape parentShape = shape.ParentShape;

                    // If the shape has a parent and its ID matches the target, record its ID.
                    if (parentShape != null && parentShape.ID == parentShapeId)
                    {
                        childShapeIds.Add(shape.ID);
                    }
                }
            }

            // Output the collected child shape IDs.
            Console.WriteLine($"Child shape IDs of parent shape {parentShapeId}:");
            foreach (long id in childShapeIds)
            {
                Console.WriteLine(id);
            }
        }
        catch (Exception ex)
        {
            // Write any unexpected errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}