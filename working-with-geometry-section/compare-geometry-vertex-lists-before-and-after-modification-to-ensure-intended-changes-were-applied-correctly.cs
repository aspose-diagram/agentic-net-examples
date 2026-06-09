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
                string inputPath = "input.vsdx"; // replace with actual file path
                Diagram diagram = new Diagram(inputPath);

                // Get the first shape on the active page
                Shape shape = diagram.ActivePage.Shapes[0];

                // Capture the original vertex list
                List<(double X, double Y)> originalVertices = GetVertexList(shape);

                // Modify geometry: add a new vertex to the first geometry path
                AddVertex(shape, 5.0, 5.0);

                // Capture the modified vertex list
                List<(double X, double Y)> modifiedVertices = GetVertexList(shape);

                // Compare the vertex lists
                if (modifiedVertices.Count != originalVertices.Count + 1)
                {
                    throw new Exception($"Vertex count mismatch. Expected {originalVertices.Count + 1}, but got {modifiedVertices.Count}.");
                }

                // Verify that the new vertex is the last one and has the expected coordinates
                var newVertex = modifiedVertices[modifiedVertices.Count - 1];
                if (Math.Abs(newVertex.X - 5.0) > 0.0001 || Math.Abs(newVertex.Y - 5.0) > 0.0001)
                {
                    throw new Exception($"New vertex coordinates are incorrect. Expected (5.0,5.0) but got ({newVertex.X},{newVertex.Y}).");
                }

                Console.WriteLine("Geometry modification verified successfully.");

                // Optionally save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        // Retrieves a flat list of vertex coordinates from all geometry paths of a shape
        private static List<(double X, double Y)> GetVertexList(Shape shape)
        {
            List<(double X, double Y)> vertices = new List<(double X, double Y)>();

            // Enumerate geometries explicitly as Geom
            foreach (Geom geom in shape.Geoms)
            {
                // Enumerate coordinate collection; items are typed as objects
                foreach (object coord in geom.CoordinateCol)
                {
                    if (coord is MoveTo move)
                    {
                        vertices.Add((move.X.Value, move.Y.Value));
                    }
                    else if (coord is LineTo line)
                    {
                        vertices.Add((line.X.Value, line.Y.Value));
                    }
                    // Additional segment types (e.g., ArcTo) can be handled similarly if needed
                }
            }

            return vertices;
        }

        // Adds a new LineTo vertex with specified coordinates to the first geometry path of the shape
        private static void AddVertex(Shape shape, double x, double y)
        {
            if (shape.Geoms.Count == 0)
            {
                throw new Exception("Shape does not contain any geometry paths.");
            }

            // Get the first geometry path
            Geom targetGeom = (Geom)shape.Geoms[0];

            // Create a new LineTo segment
            LineTo newSegment = new LineTo();
            newSegment.X.Value = x;
            newSegment.Y.Value = y;

            // Append the new segment to the coordinate collection
            targetGeom.CoordinateCol.Add(newSegment);
        }
    }