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

            // Get the first page (use indexer, not ActivePage)
            Page page = diagram.Pages[0];

            // Helper to create a diamond shape using DrawPolyline (expects a flat double array)
            Shape CreateDiamond(double centerX, double centerY, double width, double height)
            {
                // Calculate half dimensions
                double halfW = width / 2.0;
                double halfH = height / 2.0;

                // Define the diamond vertices as a flat double array (x1, y1, x2, y2, ...)
                double[] points = new double[]
                {
                    centerX, centerY - halfH,               // top
                    centerX + halfW, centerY,               // right
                    centerX, centerY + halfH,               // bottom
                    centerX - halfW, centerY,               // left
                    centerX, centerY - halfH                // close polygon
                };

                // Draw the diamond; returns a long shape ID
                long shapeId = page.DrawPolyline(points);

                // Retrieve the Shape object (cast long to int for GetShape)
                Shape shape = page.Shapes.GetShape((int)shapeId);
                return shape;
            }

            // Create multiple diamonds at different positions
            Shape diamond1 = CreateDiamond(2.0, 2.0, 1.5, 1.5);
            Shape diamond2 = CreateDiamond(5.0, 2.0, 1.5, 1.5);
            Shape diamond3 = CreateDiamond(3.5, 4.5, 1.5, 1.5);

            // Group the diamonds together
            Shape[] diamonds = new Shape[] { diamond1, diamond2, diamond3 };
            Shape groupShape = page.Shapes.Group(diamonds);

            // Export the group as a single SVG file
            SVGSaveOptions svgOptions = new SVGSaveOptions();
            groupShape.ToSvg("diamonds.svg", svgOptions);

            Console.WriteLine("Diamond group exported to diamonds.svg");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}