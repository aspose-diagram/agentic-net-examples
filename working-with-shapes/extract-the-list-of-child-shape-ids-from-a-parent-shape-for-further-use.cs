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

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Specify the page index and parent shape ID you want to inspect
                int pageIndex = 0;          // first page
                long parentShapeId = 1;     // example parent shape ID

                // Retrieve the parent shape
                Shape parentShape = diagram.Pages[pageIndex].Shapes.GetShape(parentShapeId);
                if (parentShape == null)
                {
                    throw new Exception($"Parent shape with ID {parentShapeId} not found on page {pageIndex}.");
                }

                // Collect child shape IDs
                List<long> childShapeIds = new List<long>();
                foreach (Shape child in parentShape.Shapes)
                {
                    childShapeIds.Add(child.ID);
                }

                // Output the collected IDs
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