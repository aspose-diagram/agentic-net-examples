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

            // Load an existing Visio diagram
            Diagram diagram = new Diagram(@"input.vsdx");

            // ID of the shape whose glued shapes we want to enumerate
            long targetShapeId = 1; // TODO: replace with the actual shape ID

            // Retrieve the shape object from the first page (adjust page index if needed)
            Shape targetShape = diagram.Pages[0].Shapes.GetShape(targetShapeId);

            // Get IDs of all 1‑D shapes glued to the target shape
            long[] glued1D = targetShape.GluedShapes(GluedShapesFlags.GluedShapesAll1D, null, null);

            // Get IDs of all 2‑D shapes glued to the target shape
            long[] glued2D = targetShape.GluedShapes(GluedShapesFlags.GluedShapesAll2D, null, null);

            // Combine the results into a single list
            List<long> allGluedIds = new List<long>();
            if (glued1D != null) allGluedIds.AddRange(glued1D);
            if (glued2D != null) allGluedIds.AddRange(glued2D);

            // Output the IDs of the glued shapes
            Console.WriteLine($"Shapes glued to shape ID {targetShapeId}:");
            foreach (long id in allGluedIds)
            {
                Console.WriteLine(id);
            }

            // If you modify the diagram and need to save it, uncomment the line below
            // diagram.Save(@"output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
