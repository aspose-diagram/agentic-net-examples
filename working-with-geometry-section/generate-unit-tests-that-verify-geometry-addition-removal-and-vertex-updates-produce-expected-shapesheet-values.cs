using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Create a new empty diagram (contains a default page)
                Diagram diagram = new Diagram();

                // Add a rectangle shape to the first page
                // Parameters: pinX, pinY, width, height, masterName
                long shapeId = diagram.Pages[0].AddShape(2.0, 2.0, 1.0, 1.0, "Rectangle");
                Shape shape = diagram.Pages[0].Shapes.GetShape(shapeId);

                // Ensure the shape has at least one geometry section
                if (shape.Geoms == null || shape.Geoms.Count == 0)
                    throw new Exception("Shape does not contain any geometry sections.");

                // Use the first geometry section for tests
                Geom geom = (Geom)shape.Geoms[0];
                var coordCol = geom.CoordinateCol;

                // Record initial count of geometry vertices
                int initialCount = coordCol.Count;

                // ------------------------------
                // Test 1: Add a new vertex (LineTo)
                // ------------------------------
                LineTo newVertex = new LineTo();
                newVertex.X.Value = 2.5; // X coordinate in inches
                newVertex.Y.Value = 2.5; // Y coordinate in inches
                coordCol.Add(newVertex);

                // Verify that the count increased by one
                if (coordCol.Count != initialCount + 1)
                    throw new Exception($"Vertex addition failed. Expected count {initialCount + 1}, actual {coordCol.Count}.");

                // Verify that the newly added vertex has the expected coordinates
                // The newly added vertex is the last item in the collection
                var addedVertex = (LineTo)coordCol[coordCol.Count - 1];
                if (Math.Abs(addedVertex.X.Value - 2.5) > 0.0001 || Math.Abs(addedVertex.Y.Value - 2.5) > 0.0001)
                    throw new Exception("Added vertex coordinates do not match expected values.");

                Console.WriteLine("Test 1 (Add Vertex) passed.");

                // ---------------------------------
                // Test 2: Update an existing vertex
                // ---------------------------------
                // Find the first existing LineTo vertex (skip MoveTo if present)
                LineTo firstLineTo = null;
                foreach (var item in coordCol)
                {
                    if (item is LineTo lt)
                    {
                        firstLineTo = lt;
                        break;
                    }
                }

                if (firstLineTo == null)
                    throw new Exception("No existing LineTo vertex found to update.");

                // Update its coordinates
                firstLineTo.X.Value = 3.0;
                firstLineTo.Y.Value = 3.0;

                // Verify the update
                if (Math.Abs(firstLineTo.X.Value - 3.0) > 0.0001 || Math.Abs(firstLineTo.Y.Value - 3.0) > 0.0001)
                    throw new Exception("Vertex update failed: coordinates do not match expected values.");

                Console.WriteLine("Test 2 (Update Vertex) passed.");

                // ---------------------------------
                // Test 3: Remove (logically delete) a vertex
                // ---------------------------------
                // Mark the previously added vertex as deleted
                addedVertex.Del = BOOL.True;

                // Verify the deletion flag
                if (addedVertex.Del != BOOL.True)
                    throw new Exception("Vertex removal failed: deletion flag not set.");

                Console.WriteLine("Test 3 (Remove Vertex) passed.");

                // All tests succeeded
                Console.WriteLine("All geometry unit tests completed successfully.");

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }