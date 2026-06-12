using System;
using System.IO;
using Aspose.Diagram;

class RetrieveGluedShapes
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Identify the master shape instance.
            // For this example we assume the master shape is on the first page with a known ID.
            // Replace 1 with the actual shape ID of the master shape instance.
            const long masterShapeId = 1;
            Shape masterShape = diagram.Pages[0].Shapes.GetShape(masterShapeId);

            // Retrieve IDs of all shapes glued to the master shape.
            // Using GluedShapesAll2D to get both incoming and outgoing 2‑D connections.
            long[] gluedShapeIds = masterShape.GluedShapes(
                GluedShapesFlags.GluedShapesAll2D,   // flag
                null,                               // no category filter
                null);                              // no additional shape filter

            // Write the identifiers to a CSV file.
            using (StreamWriter writer = new StreamWriter("glued_shapes.csv"))
            {
                // Header (optional)
                writer.WriteLine("GluedShapeId");

                foreach (long id in gluedShapeIds)
                {
                    writer.WriteLine(id);
                }
            }

            // Optionally save the diagram if any modifications were made.
            // diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
