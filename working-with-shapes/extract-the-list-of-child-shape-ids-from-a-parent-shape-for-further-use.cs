using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the Visio file (replace with actual file path)
                string diagramPath = "input.vsdx";

                // ID of the parent shape whose children you want to retrieve (replace with actual ID)
                long parentShapeId = 1;

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Assume the shape is on the first page; adjust if needed
                Page page = diagram.Pages[0];

                // Retrieve the parent shape by its ID
                Shape parentShape = page.Shapes.GetShape(parentShapeId);
                if (parentShape == null)
                {
                    Console.WriteLine($"Parent shape with ID {parentShapeId} not found.");
                    return;
                }

                // Collect child shape IDs (if the shape is a group)
                List<long> childShapeIds = new List<long>();
                if (parentShape.Shapes != null)
                {
                    foreach (Shape child in parentShape.Shapes)
                    {
                        childShapeIds.Add(child.ID);
                    }
                }

                // Output the collected child IDs
                Console.WriteLine($"Parent Shape ID: {parentShapeId}");
                Console.WriteLine("Child Shape IDs:");
                foreach (long id in childShapeIds)
                {
                    Console.WriteLine(id);
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }