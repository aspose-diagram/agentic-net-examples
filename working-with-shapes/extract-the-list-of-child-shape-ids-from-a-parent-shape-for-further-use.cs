using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <parentShapeName>");
            return;
        }

        string diagramPath = args[0];
        string parentShapeName = args[1];

        // Guard: ensure the diagram file exists.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Locate the parent shape by its NameU.
            Shape parentShape = null;
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.NameU != null && shape.NameU.Equals(parentShapeName, StringComparison.OrdinalIgnoreCase))
                    {
                        parentShape = shape;
                        break;
                    }
                }
                if (parentShape != null) break;
            }

            if (parentShape == null)
            {
                Console.Error.WriteLine($"Error: Parent shape with name '{parentShapeName}' not found.");
                return;
            }

            long parentId = parentShape.ID;

            // Collect IDs of child shapes whose ParentShape matches the found parent.
            List<long> childShapeIds = new List<long>();
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape has a parent and compare IDs.
                    if (shape.ParentShape != null && shape.ParentShape.ID == parentId)
                    {
                        childShapeIds.Add(shape.ID);
                    }
                }
            }

            // Output the list of child shape IDs.
            Console.WriteLine($"Parent Shape ID: {parentId}");
            Console.WriteLine("Child Shape IDs:");
            if (childShapeIds.Count == 0)
            {
                Console.WriteLine("  (none)");
            }
            else
            {
                foreach (long childId in childShapeIds)
                {
                    Console.WriteLine($"  {childId}");
                }
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}