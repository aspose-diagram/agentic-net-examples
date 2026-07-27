using System.IO;
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

            // Get the active page where shapes will be added
            Page page = diagram.ActivePage;

            // Helper method to create a diamond shape using DrawPolyline
            // points: left, top, right, bottom, left (closed)
            long CreateDiamond(double centerX, double centerY, double width, double height)
            {
                double halfW = width / 2.0;
                double halfH = height / 2.0;
                double[] points = new double[]
                {
                    centerX, centerY - halfH, // top
                    centerX + halfW, centerY, // right
                    centerX, centerY + halfH, // bottom
                    centerX - halfW, centerY, // left
                    centerX, centerY - halfH  // close polygon
                };
                return page.DrawPolyline(points);
            }

            // Create three diamonds at different locations
            long diamondId1 = CreateDiamond(2.0, 2.0, 1.0, 1.0);
            long diamondId2 = CreateDiamond(4.0, 2.0, 1.0, 1.0);
            long diamondId3 = CreateDiamond(3.0, 3.5, 1.0, 1.0);

            // Retrieve the Shape objects from their IDs
            Shape diamond1 = page.Shapes.GetShape(diamondId1);
            Shape diamond2 = page.Shapes.GetShape(diamondId2);
            Shape diamond3 = page.Shapes.GetShape(diamondId3);

            // Group the three diamonds into a single group shape
            Shape groupShape = page.Shapes.Group(new Shape[] { diamond1, diamond2, diamond3 });

            // Export the group as a single SVG file
            SVGSaveOptions svgOptions = new SVGSaveOptions();
            groupShape.ToSvg("GroupedDiamonds.svg", svgOptions);

            Console.WriteLine("Grouped diamonds exported to GroupedDiamonds.svg");

        }
        catch (System.NullReferenceException ex)
        {
            Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
        }
    }
}
