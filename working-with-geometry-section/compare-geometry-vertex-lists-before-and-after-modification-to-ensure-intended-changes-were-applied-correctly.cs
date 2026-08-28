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
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Access the first page and the first shape on that page
                Page firstPage = (Page)diagram.Pages[0];
                Shape shape = (Shape)firstPage.Shapes[0];

                // Capture the original vertex list
                List<string> originalVertices = GetVertexList(shape);

                // Modify the geometry: add a new LineTo vertex to the first geometry path
                Geom targetGeom = (Geom)shape.Geoms[0];
                LineTo newSegment = new LineTo();
                newSegment.X.Value = 2.0;
                newSegment.Y.Value = 2.0;
                targetGeom.CoordinateCol.Add(newSegment);

                // Capture the modified vertex list
                List<string> modifiedVertices = GetVertexList(shape);

                // Compare the lists to ensure the new vertex was added
                bool changeDetected = modifiedVertices.Count == originalVertices.Count + 1 &&
                                      modifiedVertices[modifiedVertices.Count - 1] == "LineTo(2,2)";

                if (!changeDetected)
                {
                    throw new Exception("Geometry modification was not applied as expected.");
                }

                Console.WriteLine("Vertex list before modification:");
                foreach (string v in originalVertices)
                {
                    Console.WriteLine(v);
                }

                Console.WriteLine("\nVertex list after modification:");
                foreach (string v in modifiedVertices)
                {
                    Console.WriteLine(v);
                }

                Console.WriteLine("\nGeometry modification verified successfully.");

                // Optionally save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        // Helper method to extract a readable list of vertices from a shape
        private static List<string> GetVertexList(Shape shape)
        {
            List<string> vertices = new List<string>();

            // Iterate over each geometry section
            foreach (Geom geom in shape.Geoms)
            {
                // Iterate over each coordinate command within the geometry
                foreach (object coord in geom.CoordinateCol)
                {
                    if (coord is MoveTo move)
                    {
                        vertices.Add($"MoveTo({move.X.Value},{move.Y.Value})");
                    }
                    else if (coord is LineTo line)
                    {
                        vertices.Add($"LineTo({line.X.Value},{line.Y.Value})");
                    }
                    // Additional command types (e.g., ArcTo) can be handled similarly if needed
                }
            }

            return vertices;
        }
    }