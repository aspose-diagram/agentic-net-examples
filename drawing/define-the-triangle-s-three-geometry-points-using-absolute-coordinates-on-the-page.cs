using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            // Create a new empty Visio diagram (contains a default page)
            using (Diagram diagram = new Diagram())
            {
                // Access the first (and only) page
                Page page = diagram.Pages[0];

                // Define absolute coordinates for the triangle vertices (in inches)
                // Vertex A (2, 2)
                // Vertex B (5, 2)
                // Vertex C (3.5, 5)
                double x1 = 2.0;
                double y1 = 2.0;
                double x2 = 5.0;
                double y2 = 2.0;

                // Additional points: C and back to A to close the shape
                double[] additionalPoints = new double[]
                {
                    3.5, 5.0,   // Vertex C
                    2.0, 2.0    // Return to Vertex A to close the triangle
                };

                // Draw the triangle using a polyline
                // The method creates a shape and returns its ID (long), which we ignore here
                page.DrawPolyline(x1, y1, x2, y2, additionalPoints);

                // Save the diagram to a VSDX file
                diagram.Save("TriangleDiagram.vsdx", SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Triangle diagram created and saved as 'TriangleDiagram.vsdx'.");
        }
    }