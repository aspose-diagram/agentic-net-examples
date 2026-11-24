using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Run geometry manipulation tests
        TestAddVertex();
        TestUpdateVertex();
        TestRemoveVertex();

        Console.WriteLine("All geometry tests passed.");
    }

    // Test adding a vertex to a shape's geometry and verify ShapeSheet values
    static void TestAddVertex()
    {
        try
        {
            // Create an empty diagram (no file required)
            using (Diagram diagram = new Diagram())
            {
                // Add a rectangle shape to the active page
                // Fourth parameter is a plain bool (not BOOL)
                long shapeId = diagram.ActivePage.AddShape(1.0, 1.0, "Rectangle", false);
                Shape shape = diagram.ActivePage.Shapes.GetShape(shapeId);

                // Ensure the shape contains at least one geometry section
                if (shape.Geoms == null || shape.Geoms.Count == 0)
                    throw new Exception("Shape does not contain any geometry sections.");

                // Retrieve the first geometry section (explicit cast to Geom)
                Geom geom = (Geom)shape.Geoms[0];

                // Record the initial number of vertices (segments)
                int initialCount = geom.CoordinateCol.Count;

                // Create a new LineTo vertex with specific coordinates
                LineTo newVertex = new LineTo();
                newVertex.X.Value = 2.5;
                newVertex.Y.Value = 3.5;

                // Append the new vertex to the geometry
                geom.CoordinateCol.Add(newVertex);

                // Verify that the vertex count increased by one
                int afterAddCount = geom.CoordinateCol.Count;
                if (afterAddCount != initialCount + 1)
                    throw new Exception($"Vertex addition failed. Expected count {initialCount + 1}, got {afterAddCount}.");

                // Verify that the last vertex has the expected coordinates
                LineTo addedVertex = geom.CoordinateCol[afterAddCount - 1] as LineTo;
                if (addedVertex == null)
                    throw new Exception("Added segment is not a LineTo instance.");

                if (Math.Abs(addedVertex.X.Value - 2.5) > 0.0001 || Math.Abs(addedVertex.Y.Value - 3.5) > 0.0001)
                    throw new Exception("Added vertex coordinates do not match expected values.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"TestAddVertex failed: {ex.Message}");
            throw;
        }
    }

    // Test updating an existing vertex and verify ShapeSheet values
    static void TestUpdateVertex()
    {
        try
        {
            using (Diagram diagram = new Diagram())
            {
                long shapeId = diagram.ActivePage.AddShape(1.0, 1.0, "Rectangle", false);
                Shape shape = diagram.ActivePage.Shapes.GetShape(shapeId);

                if (shape.Geoms == null || shape.Geoms.Count == 0)
                    throw new Exception("Shape does not contain any geometry sections.");

                Geom geom = (Geom)shape.Geoms[0];

                // Ensure there is at least one segment to update
                if (geom.CoordinateCol.Count == 0)
                    throw new Exception("Geometry has no segments to update.");

                // Attempt to update the first segment (could be MoveTo or LineTo)
                object firstSeg = geom.CoordinateCol[0];
                if (firstSeg is LineTo lineToSeg)
                {
                    lineToSeg.X.Value = 1.0;
                    lineToSeg.Y.Value = 1.0;

                    if (Math.Abs(lineToSeg.X.Value - 1.0) > 0.0001 || Math.Abs(lineToSeg.Y.Value - 1.0) > 0.0001)
                        throw new Exception("Vertex update failed: coordinates do not match expected values.");
                }
                else if (firstSeg is MoveTo moveToSeg)
                {
                    moveToSeg.X.Value = 1.0;
                    moveToSeg.Y.Value = 1.0;

                    if (Math.Abs(moveToSeg.X.Value - 1.0) > 0.0001 || Math.Abs(moveToSeg.Y.Value - 1.0) > 0.0001)
                        throw new Exception("Vertex update failed: coordinates do not match expected values.");
                }
                else
                {
                    throw new Exception("First geometry segment is neither LineTo nor MoveTo.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"TestUpdateVertex failed: {ex.Message}");
            throw;
        }
    }

    // Test removing a vertex from a shape's geometry and verify ShapeSheet values
    static void TestRemoveVertex()
    {
        try
        {
            using (Diagram diagram = new Diagram())
            {
                long shapeId = diagram.ActivePage.AddShape(1.0, 1.0, "Rectangle", false);
                Shape shape = diagram.ActivePage.Shapes.GetShape(shapeId);

                if (shape.Geoms == null || shape.Geoms.Count == 0)
                    throw new Exception("Shape does not contain any geometry sections.");

                Geom geom = (Geom)shape.Geoms[0];

                // Add a temporary vertex to ensure there is something to remove
                LineTo tempVertex = new LineTo();
                tempVertex.X.Value = 5.0;
                tempVertex.Y.Value = 5.0;
                geom.CoordinateCol.Add(tempVertex);

                int countBeforeRemove = geom.CoordinateCol.Count;
                if (countBeforeRemove == 0)
                    throw new Exception("No vertices available to remove.");

                // Remove the last vertex
                geom.CoordinateCol.RemoveAt(countBeforeRemove - 1);

                // Verify that the count decreased by one
                int countAfterRemove = geom.CoordinateCol.Count;
                if (countAfterRemove != countBeforeRemove - 1)
                    throw new Exception($"Vertex removal failed. Expected count {countBeforeRemove - 1}, got {countAfterRemove}.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"TestRemoveVertex failed: {ex.Message}");
            throw;
        }
    }
}