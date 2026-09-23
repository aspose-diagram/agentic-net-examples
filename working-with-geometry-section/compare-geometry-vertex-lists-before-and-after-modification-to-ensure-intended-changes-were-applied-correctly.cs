using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Load an existing Visio diagram
                Diagram diagram = new Diagram("input.vsdx");

                // Find the first non-page shape on the first page
                Shape targetShape = null;
                for (int s = 0; s < diagram.Pages[0].Shapes.Count; s++)
                {
                    Shape shp = diagram.Pages[0].Shapes[s];
                    if (shp.ID != 0) // page shape has ID 0
                    {
                        targetShape = shp;
                        break;
                    }
                }

                if (targetShape == null)
                {
                    throw new Exception("No suitable shape found in the diagram.");
                }

                // Capture vertex list before modification
                List<(double X, double Y)> beforeVertices = GetVertices(targetShape);

                // Modify geometry: add a new vertex at (5.0, 5.0)
                Geom geom = (Geom)targetShape.Geoms[0];
                LineTo newVertex = new LineTo();
                newVertex.X.Value = 5.0;
                newVertex.Y.Value = 5.0;
                geom.CoordinateCol.Add(newVertex);

                // Capture vertex list after modification
                List<(double X, double Y)> afterVertices = GetVertices(targetShape);

                // Verify that the new vertex was added correctly
                if (afterVertices.Count != beforeVertices.Count + 1)
                {
                    throw new Exception($"Vertex count mismatch. Expected {beforeVertices.Count + 1}, but got {afterVertices.Count}.");
                }

                // Ensure original vertices are unchanged and new vertex is at the end
                for (int i = 0; i < beforeVertices.Count; i++)
                {
                    if (Math.Abs(beforeVertices[i].X - afterVertices[i].X) > 1e-6 ||
                        Math.Abs(beforeVertices[i].Y - afterVertices[i].Y) > 1e-6)
                    {
                        throw new Exception($"Vertex at index {i} was altered unexpectedly.");
                    }
                }

                // Check the newly added vertex
                (double X, double Y) newAdded = afterVertices[afterVertices.Count - 1];
                if (Math.Abs(newAdded.X - 5.0) > 1e-6 || Math.Abs(newAdded.Y - 5.0) > 1e-6)
                {
                    throw new Exception("The newly added vertex does not have the expected coordinates (5.0, 5.0).");
                }

                Console.WriteLine("Geometry vertex list comparison succeeded. Modification applied as intended.");

                // Save the modified diagram
                diagram.Save("output_modified.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        // Retrieves the list of vertex coordinates (MoveTo and LineTo) from the first geometry of a shape
        static List<(double X, double Y)> GetVertices(Shape shape)
        {
            List<(double X, double Y)> vertices = new List<(double X, double Y)>();

            // Assume the shape has at least one geometry section
            Geom geom = (Geom)shape.Geoms[0];

            for (int i = 0; i < geom.CoordinateCol.Count; i++)
            {
                object segment = geom.CoordinateCol[i];

                if (segment is MoveTo)
                {
                    MoveTo move = (MoveTo)segment;
                    vertices.Add((move.X.Value, move.Y.Value));
                }
                else if (segment is LineTo)
                {
                    LineTo line = (LineTo)segment;
                    vertices.Add((line.X.Value, line.Y.Value));
                }
                // Other segment types (ArcTo, etc.) are ignored for this comparison
            }

            return vertices;
        }
    }