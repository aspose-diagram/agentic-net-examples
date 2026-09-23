using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Add a single page to the diagram
            diagram.Pages.Add(new Page());
            Page page = diagram.Pages[0];

            // Dictionary to keep track of the vertices used for each shape
            Dictionary<long, double[]> shapeVertices = new Dictionary<long, double[]>();

            // Define pentagon vertices (grid-aligned coordinates)
            // Points are defined as a flat array: x1, y1, x2, y2, ..., xn, yn
            double[] pentagonPoints = new double[]
            {
                0, 0,   // Vertex 1
                2, 0,   // Vertex 2
                3, 2,   // Vertex 3
                1, 4,   // Vertex 4
                -1, 2   // Vertex 5
            };

            // Draw the pentagon shape on the page
            long pentagonId = page.DrawPolyline(pentagonPoints);

            // Store the vertices for later validation
            shapeVertices[pentagonId] = pentagonPoints;

            // Validate that all pentagon vertices align with the expected grid (integer coordinates)
            foreach (Shape shape in page.Shapes)
            {
                if (!shapeVertices.ContainsKey(shape.ID))
                    continue; // Skip shapes that are not our pentagons

                double[] vertices = shapeVertices[shape.ID];

                for (int i = 0; i < vertices.Length; i += 2)
                {
                    double x = vertices[i];
                    double y = vertices[i + 1];

                    // Check if X and Y are integer values (grid alignment)
                    if (x % 1 != 0 || y % 1 != 0)
                    {
                        throw new Exception(
                            $"Pentagon shape ID {shape.ID} has a vertex at ({x}, {y}) which does not align to the integer grid.");
                    }
                }
            }

            // Save the diagram to a VSDX file
            diagram.Save("PentagonDiagram.vsdx", SaveFileFormat.Vsdx);

            Console.WriteLine("Pentagon vertices validated and diagram saved successfully.");
        }
    }