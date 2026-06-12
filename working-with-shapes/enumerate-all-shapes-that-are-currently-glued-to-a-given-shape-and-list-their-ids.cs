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

            // Load an existing Visio diagram (replace with actual file path)
            Diagram diagram = new Diagram("input.vsdx");

            // ID of the shape whose glued shapes we want to enumerate
            long targetShapeId = 1; // replace with the actual shape ID

            // Locate the shape in the diagram pages
            Shape targetShape = null;
            foreach (Page page in diagram.Pages)
            {
                targetShape = page.Shapes.GetShape(targetShapeId);
                if (targetShape != null)
                    break;
            }

            if (targetShape == null)
            {
                Console.WriteLine($"Shape with ID {targetShapeId} not found.");
                return;
            }

            // Retrieve IDs of all 1‑D shapes glued to the target shape
            long[] glued1D = targetShape.GluedShapes(GluedShapesFlags.GluedShapesAll1D, null, null);

            // Retrieve IDs of all 2‑D shapes glued to the target shape (and vice‑versa)
            long[] glued2D = targetShape.GluedShapes(GluedShapesFlags.GluedShapesAll2D, null, null);

            // Combine the results
            List<long> allGluedIds = new List<long>();
            if (glued1D != null) allGluedIds.AddRange(glued1D);
            if (glued2D != null) allGluedIds.AddRange(glued2D);

            // Output the IDs
            Console.WriteLine($"Shapes glued to shape ID {targetShapeId}:");
            foreach (long id in allGluedIds)
            {
                Console.WriteLine(id);
            }

            // Save the diagram if any modifications were made (optional)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
