using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Path to the source Visio file
        string inputPath = "input.vsdx";
        // Guard: ensure the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Path to the output Visio file
        string outputPath = "output.vsdx";

        try
        {
            // Load the diagram from the input file
            Diagram diagram = new Diagram(inputPath);

            // Assume we work with the first page
            Page page = diagram.Pages[0];

            // IDs of the source and target shapes (replace with actual IDs as needed)
            long sourceShapeId = 1;
            long targetShapeId = 2;

            // Retrieve the source shape by ID
            Shape sourceShape = page.Shapes.GetShape(sourceShapeId);
            if (sourceShape == null)
            {
                throw new Exception($"Source shape with ID {sourceShapeId} not found.");
            }

            // Retrieve the target shape by ID
            Shape targetShape = page.Shapes.GetShape(targetShapeId);
            if (targetShape == null)
            {
                throw new Exception($"Target shape with ID {targetShapeId} not found.");
            }

            // Clear existing geometry of the target shape
            targetShape.Geoms.Clear();

            // Clone geometry from source to target
            foreach (Geom sourceGeom in sourceShape.Geoms)
            {
                // Create a new Geom instance for the target shape
                Geom clonedGeom = new Geom();

                // Copy each coordinate (MoveTo, LineTo, etc.) from the source geometry
                foreach (object coord in sourceGeom.CoordinateCol)
                {
                    // Ensure the object is a Coordinate before adding
                    if (coord is Coordinate c)
                    {
                        // Add the same coordinate instance (sufficient for a simple clone)
                        clonedGeom.CoordinateCol.Add(c);
                    }
                }

                // Add the cloned geometry to the target shape
                targetShape.Geoms.Add(clonedGeom);
            }

            // Save the modified diagram to the output file using VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}