using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            // Create a new empty diagram
            using (Diagram diagram = new Diagram())
            {
                // Get the first page (default page exists in a new diagram)
                var page = diagram.Pages[0];

                // Helper to create a diamond shape using DrawPolyline
                // Diamond points: top, right, bottom, left, back to top (closed)
                long CreateDiamond(double centerX, double centerY, double size)
                {
                    double half = size / 2.0;
                    // Coordinates: (centerX, centerY - half), (centerX + half, centerY),
                    // (centerX, centerY + half), (centerX - half, centerY), back to start
                    double[] points = new double[]
                    {
                        centerX, centerY - half,
                        centerX + half, centerY,
                        centerX, centerY + half,
                        centerX - half, centerY,
                        centerX, centerY - half
                    };
                    return page.DrawPolyline(points);
                }

                // Create three diamonds at different positions
                long id1 = CreateDiamond(5.0, 5.0, 2.0);
                long id2 = CreateDiamond(8.0, 5.0, 2.0);
                long id3 = CreateDiamond(6.5, 8.0, 2.0);

                // Retrieve Shape objects from their IDs
                Shape diamond1 = page.Shapes.GetShape(id1);
                Shape diamond2 = page.Shapes.GetShape(id2);
                Shape diamond3 = page.Shapes.GetShape(id3);

                // Group the diamonds into a single group shape
                Shape groupShape = page.Shapes.Group(new Shape[] { diamond1, diamond2, diamond3 });

                // Export the group as a standalone SVG file
                string outputSvgPath = "GroupedDiamonds.svg";
                SVGSaveOptions svgOptions = new SVGSaveOptions();
                groupShape.ToSvg(outputSvgPath, svgOptions);

                // Optional: save the whole diagram for reference
                diagram.Save("DiagramWithGroup.vsdx", SaveFileFormat.Vsdx);
            }
        }
    }