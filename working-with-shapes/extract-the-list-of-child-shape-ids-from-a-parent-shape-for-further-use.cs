using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: expect diagram file path and parent shape name
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <parentShapeNameU>");
            return;
        }

        string diagramPath = args[0];
        string parentShapeNameU = args[1];

        // Guard: check if the diagram file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Locate the parent shape by its NameU (case-sensitive)
            Shape parentShape = null;
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.NameU == parentShapeNameU)
                    {
                        parentShape = shape;
                        break;
                    }
                }
                if (parentShape != null) break;
            }

            if (parentShape == null)
            {
                Console.Error.WriteLine($"Error: Parent shape with NameU '{parentShapeNameU}' not found.");
                return;
            }

            long parentId = parentShape.ID;

            // Collect IDs of child shapes (shapes whose ParentShape.ID matches the parent ID)
            List<long> childShapeIds = new List<long>();
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape has a parent and compare IDs
                    if (shape.ParentShape != null && shape.ParentShape.ID == parentId)
                    {
                        childShapeIds.Add(shape.ID);
                    }
                }
            }

            // Output the result
            if (childShapeIds.Count == 0)
            {
                Console.WriteLine($"No child shapes found for parent shape '{parentShapeNameU}' (ID: {parentId}).");
            }
            else
            {
                Console.WriteLine($"Child shape IDs for parent shape '{parentShapeNameU}' (ID: {parentId}):");
                foreach (long id in childShapeIds)
                {
                    Console.WriteLine(id);
                }
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}