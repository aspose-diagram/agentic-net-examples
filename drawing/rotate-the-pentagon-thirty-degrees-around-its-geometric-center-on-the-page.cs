using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing;

class Program
{
    static void Main(string[] args)
    {
        // Create a new blank diagram inside a try/catch to handle Aspose errors
        try
        {
            using (Diagram diagram = new Diagram())
            {
                // Access the first (default) page
                Page page = diagram.Pages[0];

                // Define pentagon parameters (center, radius, number of sides)
                double centerX = 5.0;   // inches
                double centerY = 5.0;   // inches
                double radius = 2.0;    // inches
                int sides = 5;
                double angleOffset = Math.PI / 2; // start at top

                // Build a flat double array of coordinates (x1, y1, x2, y2, ...) 
                // Include the first point again at the end to close the shape
                double[] coords = new double[(sides + 1) * 2];
                for (int i = 0; i <= sides; i++)
                {
                    double angle = angleOffset + i * 2 * Math.PI / sides;
                    double x = centerX + radius * Math.Cos(angle);
                    double y = centerY + radius * Math.Sin(angle);
                    coords[i * 2] = x;
                    coords[i * 2 + 1] = y;
                }

                // Draw the pentagon as a closed polyline using the flat double array overload
                long shapeId = page.DrawPolyline(coords);

                // Retrieve the shape object by its ID
                Shape pentagon = page.Shapes.GetShape(shapeId);

                // Rotate the pentagon 30 degrees around its geometric center
                pentagon.SetAngle(30.0);

                // Save the diagram to a VSDX file
                string outputPath = "RotatedPentagon.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}