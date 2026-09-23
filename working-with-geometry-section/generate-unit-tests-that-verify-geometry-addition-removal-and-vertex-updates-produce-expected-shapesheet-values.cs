using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

public class Program
{
    public static void Main()
    {
        // Create an empty diagram
        Diagram diagram = new Diagram();

        // Add a new page to the diagram
        diagram.Pages.Add(new Page());
        Page page = diagram.Pages[0];

        // Draw a simple rectangle (pinX, pinY, width, height)
        // This method returns the shape ID (long)
        long rectShapeId = page.DrawRectangle(1.0, 1.0, 2.0, 1.0);
        Shape rectShape = page.Shapes.GetShape(rectShapeId);

        // Ensure the shape has at least one geometry section
        if (rectShape.Geoms == null || rectShape.Geoms.Count == 0)
        {
            throw new Exception("Rectangle shape does not contain any geometry sections.");
        }

        // Retrieve the first geometry (Geom) object
        Geom geom = (Geom)rectShape.Geoms[0];

        // Record the initial number of coordinate entries
        int initialCount = geom.CoordinateCol.Count;

        // -------------------------------------------------
        // 1. Add a new vertex (LineTo) to the geometry
        // -------------------------------------------------
        LineTo newVertex = new LineTo();
        newVertex.X.Value = 3.0; // X coordinate
        newVertex.Y.Value = 2.0; // Y coordinate
        geom.CoordinateCol.Add(newVertex);

        // Verify that the count increased by one
        int afterAddCount = geom.CoordinateCol.Count;
        if (afterAddCount != initialCount + 1)
        {
            throw new Exception($"Vertex addition failed. Expected count {initialCount + 1}, but got {afterAddCount}.");
        }
        Console.WriteLine("Vertex addition verified.");

        // -------------------------------------------------
        // 2. Update the newly added vertex coordinates
        // -------------------------------------------------
        newVertex.X.Value = 4.5;
        newVertex.Y.Value = 3.5;

        // Verify that the coordinates were updated
        if (Math.Abs(newVertex.X.Value - 4.5) > 0.0001 || Math.Abs(newVertex.Y.Value - 3.5) > 0.0001)
        {
            throw new Exception("Vertex coordinate update failed.");
        }
        Console.WriteLine("Vertex coordinate update verified.");

        // -------------------------------------------------
        // 3. Mark the vertex as deleted
        // -------------------------------------------------
        newVertex.Del = BOOL.True;

        // Verify the deletion flag
        if (newVertex.Del != BOOL.True)
        {
            throw new Exception("Vertex deletion flag was not set correctly.");
        }
        Console.WriteLine("Vertex deletion flag verified.");

        // -------------------------------------------------
        // Save the diagram (optional, demonstrates correct save usage)
        // -------------------------------------------------
        diagram.Save("GeometryTestOutput.vsdx", SaveFileFormat.Vsdx);
        Console.WriteLine("Diagram saved successfully.");
    }
}
