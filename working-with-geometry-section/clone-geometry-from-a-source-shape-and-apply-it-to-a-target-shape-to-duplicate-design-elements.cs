using System.IO;
using System;
using Aspose.Diagram;

public class Program
{
    public static void Main()
    {
        try
        {

            // Load the source Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Work with the first page (adjust if needed)
            Page page = diagram.Pages[0];

            // IDs of the shapes to copy from and to (replace with actual IDs)
            long sourceShapeId = 1;
            long targetShapeId = 2;

            // Retrieve the source and target shapes
            Shape sourceShape = page.Shapes.GetShape(sourceShapeId);
            Shape targetShape = page.Shapes.GetShape(targetShapeId);

            if (sourceShape == null)
            {
                throw new Exception($"Source shape with ID {sourceShapeId} not found.");
            }
            if (targetShape == null)
            {
                throw new Exception($"Target shape with ID {targetShapeId} not found.");
            }

            // Remove any existing geometry from the target shape
            targetShape.Geoms.Clear();

            // Clone geometry sections from the source shape to the target shape
            foreach (Geom geom in sourceShape.Geoms)
            {
                // Add each geometry section to the target shape
                targetShape.Geoms.Add(geom);
            }

            // Save the updated diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
            Console.WriteLine("Geometry cloned successfully and diagram saved.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
