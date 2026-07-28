using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input diagram file path (first argument) or default value
                string diagramPath = args.Length > 0 ? args[0] : "input.vsdx";

                // Target shape ID (second argument) or default value
                long targetShapeId = args.Length > 1 ? long.Parse(args[1]) : 1L;

                // Load the Visio diagram
                Diagram diagram = new Diagram(diagramPath);

                // Use the first page (adjust if needed)
                Page page = diagram.Pages[0];

                // Retrieve the shape with the specified ID
                Shape targetShape = page.Shapes.GetShape(targetShapeId);
                if (targetShape == null)
                {
                    throw new Exception($"Shape with ID {targetShapeId} not found on page {page.ID}.");
                }

                // Get IDs of all shapes glued to the target shape.
                // GluedShapesFlags.GluedShapesAll1D retrieves all 1‑D (connector) glued shapes.
                long[] gluedShapeIds = targetShape.GluedShapes(GluedShapesFlags.GluedShapesAll1D, null, null);

                // Output the result
                Console.WriteLine($"Shape ID {targetShapeId} has {gluedShapeIds.Length} glued shape(s):");
                foreach (long id in gluedShapeIds)
                {
                    Console.WriteLine($"- Glued Shape ID: {id}");
                }

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }