using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file (adjust as needed)
                string diagramPath = "example.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Choose a shape to inspect – here we take the first shape on the first page
                Page page = diagram.Pages[0];
                if (page.Shapes.Count == 0)
                {
                    Console.WriteLine("No shapes found on the first page.");
                    return;
                }

                // Retrieve the shape by its ID
                Shape shape = page.Shapes.GetShape(page.Shapes[0].ID);

                // Get IDs of all shapes connected to this shape
                // ConnectedShapesFlags.ConnectedShapesAllNodes retrieves all directly connected shapes
                long[] connectedShapeIds = shape.ConnectedShapes(ConnectedShapesFlags.ConnectedShapesAllNodes, null);

                // Output the results
                Console.WriteLine($"Shape ID {shape.ID} is connected to {connectedShapeIds.Length} shape(s):");
                foreach (long id in connectedShapeIds)
                {
                    Console.WriteLine($" - Connected Shape ID: {id}");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }