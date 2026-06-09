using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Assume both shapes are on the first page (index 0)
            Page page = diagram.Pages[0];

            // IDs of the source and target shapes (replace with actual IDs)
            long sourceShapeId = 1; // ID of the shape to copy geometry from
            long targetShapeId = 2; // ID of the shape to receive the geometry

            // Retrieve the shapes using their IDs
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

            // Clear existing geometry of the target shape by marking each existing Geom as deleted
            foreach (Geom geom in targetShape.Geoms)
            {
                geom.Del = BOOL.True;
            }

            // Copy geometry from source shape to target shape
            foreach (Geom sourceGeom in sourceShape.Geoms)
            {
                // Create a new Geom instance and copy its properties
                Geom newGeom = new Geom();

                // Copy the coordinate collection (MoveTo, LineTo, etc.)
                foreach (Coordinate coord in sourceGeom.CoordinateCol)
                {
                    // Add the coordinate to the new geometry
                    newGeom.CoordinateCol.Add(coord);
                }

                // Add the new geometry to the target shape
                targetShape.Geoms.Add(newGeom);
            }

            // Save the modified diagram to a new file
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine("Geometry cloned successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}