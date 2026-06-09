using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Use the first page (created by default)
            Page page = diagram.Pages[0];

            // Draw a simple rectangle (PinX, PinY, Width, Height)
            long rectId = page.DrawRectangle(2.0, 2.0, 4.0, 2.0);
            Shape rectShape = page.Shapes.GetShape(rectId);

            // -------------------------------------------------
            // Test 1: Add a new vertex (LineTo) to the geometry
            // -------------------------------------------------
            // Access the first geometry section
            Geom geom = (Geom)rectShape.Geoms[0];
            var coordCol = geom.CoordinateCol;

            int countBeforeAdd = coordCol.Count;

            // Create a new vertex at (3.0, 3.0)
            LineTo newVertex = new LineTo();
            newVertex.X.Value = 3.0;
            newVertex.Y.Value = 3.0;
            coordCol.Add(newVertex);

            // Verify the vertex was added
            if (coordCol.Count != countBeforeAdd + 1)
                throw new Exception("Vertex addition failed: count mismatch.");

            // Verify the added vertex coordinates directly
            AssertEqual(3.0, newVertex.X.Value, "Added vertex X coordinate incorrect.");
            AssertEqual(3.0, newVertex.Y.Value, "Added vertex Y coordinate incorrect.");

            // -------------------------------------------------
            // Test 2: Mark the newly added vertex as deleted
            // -------------------------------------------------
            newVertex.Del = BOOL.True;

            if (newVertex.Del != BOOL.True)
                throw new Exception("Vertex deletion flag not set correctly.");

            // -------------------------------------------------
            // Test 3: Update an existing vertex coordinates
            // -------------------------------------------------
            // The rectangle geometry starts with a MoveTo followed by LineTo segments.
            // Update the second vertex (first LineTo after MoveTo)
            LineTo vertexToUpdate = (LineTo)coordCol[1];
            vertexToUpdate.X.Value = 5.5;
            vertexToUpdate.Y.Value = 6.5;

            // Verify the update
            AssertEqual(5.5, vertexToUpdate.X.Value, "Vertex X update failed.");
            AssertEqual(6.5, vertexToUpdate.Y.Value, "Vertex Y update failed.");

            Console.WriteLine("All geometry tests passed successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Simple equality check with tolerance for double values
    static void AssertEqual(double expected, double actual, string message)
    {
        const double Tolerance = 1e-6;
        if (Math.Abs(expected - actual) > Tolerance)
            throw new Exception($"{message} Expected: {expected}, Actual: {actual}");
    }
}