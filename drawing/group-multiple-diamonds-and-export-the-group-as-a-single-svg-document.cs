using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Get the first page of the diagram
            Page page = diagram.Pages[0];

            // Helper method to create a diamond shape using DrawPolyline (expects a flat double array)
            long CreateDiamond(double centerX, double centerY, double width, double height)
            {
                // Calculate half dimensions
                double halfW = width / 2;
                double halfH = height / 2;

                // Define the diamond vertices as a flat double array (x1, y1, x2, y2, ...)
                double[] points = new double[]
                {
                    centerX, centerY - halfH,               // Top
                    centerX + halfW, centerY,               // Right
                    centerX, centerY + halfH,               // Bottom
                    centerX - halfW, centerY,               // Left
                    centerX, centerY - halfH                // Close back to Top
                };

                // Draw the diamond and return its shape ID
                return page.DrawPolyline(points);
            }

            // Create three diamonds at different positions
            long diamondId1 = CreateDiamond(2.0, 2.0, 1.5, 1.5);
            long diamondId2 = CreateDiamond(5.0, 2.0, 1.5, 1.5);
            long diamondId3 = CreateDiamond(3.5, 4.0, 1.5, 1.5);

            // Retrieve the Shape objects from their IDs
            Shape diamond1 = page.Shapes.GetShape(diamondId1);
            Shape diamond2 = page.Shapes.GetShape(diamondId2);
            Shape diamond3 = page.Shapes.GetShape(diamondId3);

            // Group the three diamonds into a single group shape
            Shape groupShape = page.Shapes.Group(new Shape[] { diamond1, diamond2, diamond3 });

            // Export the group as a standalone SVG file
            SVGSaveOptions svgOptions = new SVGSaveOptions();
            groupShape.ToSvg("GroupedDiamonds.svg", svgOptions);

            // Optional: Save the whole diagram for reference
            diagram.Save("DiagramWithGroupedDiamonds.vsdx", SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}