using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            // Create a new empty diagram
            using (Diagram diagram = new Diagram())
            {
                // Access the first page (index 0)
                Page page = diagram.Pages[0];

                // Define custom position (center) and size (radius) for the pentagon
                double centerX = 5.0;   // inches
                double centerY = 5.0;   // inches
                double radius = 2.0;    // inches

                // Calculate the five vertices of a regular pentagon and close the shape by repeating the first point
                double[] points = new double[]
                {
                    // Vertex 1
                    centerX + radius * Math.Cos(-Math.PI / 2),               // X1
                    centerY + radius * Math.Sin(-Math.PI / 2),               // Y1

                    // Vertex 2
                    centerX + radius * Math.Cos(-Math.PI / 2 + 2 * Math.PI / 5),
                    centerY + radius * Math.Sin(-Math.PI / 2 + 2 * Math.PI / 5),

                    // Vertex 3
                    centerX + radius * Math.Cos(-Math.PI / 2 + 4 * Math.PI / 5),
                    centerY + radius * Math.Sin(-Math.PI / 2 + 4 * Math.PI / 5),

                    // Vertex 4
                    centerX + radius * Math.Cos(-Math.PI / 2 + 6 * Math.PI / 5),
                    centerY + radius * Math.Sin(-Math.PI / 2 + 6 * Math.PI / 5),

                    // Vertex 5
                    centerX + radius * Math.Cos(-Math.PI / 2 + 8 * Math.PI / 5),
                    centerY + radius * Math.Sin(-Math.PI / 2 + 8 * Math.PI / 5),

                    // Close the polygon (repeat first vertex)
                    centerX + radius * Math.Cos(-Math.PI / 2),
                    centerY + radius * Math.Sin(-Math.PI / 2)
                };

                // Draw the pentagon using a polyline (closed shape)
                long shapeId = page.DrawPolyline(points);

                // Retrieve the shape object to apply formatting
                Shape pentagon = page.Shapes.GetShape((int)shapeId);

                // Set fill color (red) and line color (black) with a thin line weight
                pentagon.Fill.FillForegnd.Value = "#FF0000";          // Red fill
                pentagon.Line.LineColor.Value = "#000000";          // Black outline
                pentagon.Line.LineWeight.Value = 0.02;              // Line weight in inches

                // Add a label inside the pentagon
                pentagon.Text.Value.Clear();
                pentagon.Text.Value.Add(new Txt("Pentagon"));

                // Save the diagram to a VSDX file
                diagram.Save("Pentagon.vsdx", SaveFileFormat.Vsdx);
            }
        }
    }