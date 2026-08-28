using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Create a new empty diagram
                Diagram diagram = new Diagram();

                // Add a new page to the diagram
                diagram.Pages.Add(new Page());

                // Get the first (and only) page
                Page page = diagram.Pages[0];

                // Add a rectangle shape to the page
                // Parameters: PinX, PinY, Width, Height, MasterName
                long shapeId = page.AddShape(2.0, 2.0, 1.0, 1.0, "Rectangle");

                // Retrieve the shape instance
                Shape shape = page.Shapes.GetShape(shapeId);

                // Ensure the shape has at least one geometry section
                if (shape.Geoms.Count == 0)
                    throw new Exception("Shape does not contain any geometry sections.");

                // Get the first geometry (Geom) object
                Geom geom = (Geom)shape.Geoms[0];

                // Record initial vertex count
                int initialCount = geom.CoordinateCol.Count;

                // ------------------------------
                // Test: Add a new vertex (LineTo)
                // ------------------------------
                LineTo newVertex = new LineTo();
                newVertex.X.Value = 2.5; // X coordinate in inches
                newVertex.Y.Value = 2.5; // Y coordinate in inches
                geom.CoordinateCol.Add(newVertex);

                // Verify the vertex count increased by one
                int afterAddCount = geom.CoordinateCol.Count;
                if (afterAddCount != initialCount + 1)
                    throw new Exception($"Vertex addition failed. Expected count {initialCount + 1}, got {afterAddCount}.");

                Console.WriteLine("Vertex addition test passed.");

                // ---------------------------------
                // Test: Update an existing vertex
                // ---------------------------------
                // Update the newly added vertex (last element)
                LineTo addedVertex = (LineTo)geom.CoordinateCol[afterAddCount - 1];
                addedVertex.X.Value = 3.0;
                addedVertex.Y.Value = 3.0;

                // Verify the update
                if (Math.Abs(addedVertex.X.Value - 3.0) > 0.0001 || Math.Abs(addedVertex.Y.Value - 3.0) > 0.0001)
                    throw new Exception("Vertex update failed. Coordinates do not match expected values.");

                Console.WriteLine("Vertex update test passed.");

                // ---------------------------------
                // Test: Remove (logically delete) a vertex
                // ---------------------------------
                // Mark the first geometry segment as deleted
                // Typically the first segment is a MoveTo; we set its Del flag
                var firstSegment = geom.CoordinateCol[0];
                firstSegment.Del = BOOL.True;

                // Verify the deletion flag
                if (firstSegment.Del != BOOL.True)
                    throw new Exception("Vertex removal failed. Deletion flag not set.");

                Console.WriteLine("Vertex removal test passed.");

                // Optional: Save the diagram to verify no runtime errors during save
                // (File will be created in the working directory)
                diagram.Save("GeometryTestOutput.vsdx", SaveFileFormat.Vsdx);

                Console.WriteLine("All geometry tests completed successfully.");

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }