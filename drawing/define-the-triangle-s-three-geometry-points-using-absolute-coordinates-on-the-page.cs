using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create a new empty diagram
        using (Diagram diagram = new Diagram())
        {
            // Access the default page
            Page page = diagram.Pages[0];

            // Define the three vertices of the triangle using absolute coordinates (in inches)
            double[] trianglePoints = new double[]
            {
                2.0, 2.0,   // Vertex 1
                5.0, 2.0,   // Vertex 2
                3.5, 5.0,   // Vertex 3
                2.0, 2.0    // Close the shape by returning to Vertex 1
            };

            // Draw the triangle as a closed polyline
            long shapeId = page.DrawPolyline(trianglePoints);

            // Retrieve the shape for any further modifications (optional)
            Shape triangle = page.Shapes.GetShape((int)shapeId);

            // Example: add a label to the triangle
            triangle.Text.Value.Clear();
            triangle.Text.Value.Add(new Txt("Triangle"));

            // Save the diagram to a VSDX file
            diagram.Save("Triangle.vsdx", SaveFileFormat.Vsdx);
        }
    }
}
