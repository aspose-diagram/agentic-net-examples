using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                Diagram diagram = new Diagram("input.vsdx");

                // Access the first page and first shape
                Page page = diagram.Pages[0];
                Shape shape = page.Shapes[0];

                // Capture vertex list before modification
                List<(double X, double Y)> beforeVertices = GetVertexList(shape);

                // Modify geometry: add a new LineTo vertex to the first geometry path
                Geom firstGeom = (Geom)shape.Geoms[0];
                LineTo newVertex = new LineTo();
                newVertex.X.Value = 5.0;
                newVertex.Y.Value = 5.0;
                firstGeom.CoordinateCol.Add(newVertex);

                // Capture vertex list after modification
                List<(double X, double Y)> afterVertices = GetVertexList(shape);

                // Compare the two vertex lists
                if (beforeVertices.Count + 1 != afterVertices.Count)
                {
                    throw new Exception("Vertex count mismatch after modification.");
                }

                for (int i = 0; i < beforeVertices.Count; i++)
                {
                    (double X, double Y) before = beforeVertices[i];
                    (double X, double Y) after = afterVertices[i];
                    if (Math.Abs(before.X - after.X) > 1e-6 || Math.Abs(before.Y - after.Y) > 1e-6)
                    {
                        throw new Exception($"Vertex at index {i} differs after modification.");
                    }
                }

                // Verify the newly added vertex matches expected values
                (double X, double Y) addedVertex = afterVertices[afterVertices.Count - 1];
                if (Math.Abs(addedVertex.X - 5.0) > 1e-6 || Math.Abs(addedVertex.Y - 5.0) > 1e-6)
                {
                    throw new Exception("Added vertex does not have the expected coordinates.");
                }

                Console.WriteLine("Geometry vertex list comparison succeeded. Modification applied correctly.");

                // Save the modified diagram
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        // Helper method to extract all vertex coordinates from a shape's geometry
        private static List<(double X, double Y)> GetVertexList(Shape shape)
        {
            List<(double X, double Y)> vertices = new List<(double X, double Y)>();

            for (int geomIndex = 0; geomIndex < shape.Geoms.Count; geomIndex++)
            {
                Geom geom = (Geom)shape.Geoms[geomIndex];
                for (int segIndex = 0; segIndex < geom.CoordinateCol.Count; segIndex++)
                {
                    object segment = geom.CoordinateCol[segIndex];

                    MoveTo moveTo = segment as MoveTo;
                    if (moveTo != null)
                    {
                        vertices.Add((moveTo.X.Value, moveTo.Y.Value));
                        continue;
                    }

                    LineTo lineTo = segment as LineTo;
                    if (lineTo != null)
                    {
                        vertices.Add((lineTo.X.Value, lineTo.Y.Value));
                        continue;
                    }

                    // Other segment types (e.g., ArcTo) can be handled similarly if needed
                }
            }

            return vertices;
        }
    }