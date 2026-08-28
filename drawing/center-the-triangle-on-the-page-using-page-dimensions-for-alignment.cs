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
                // Access the first (default) page
                Page page = diagram.Pages[0];

                // Retrieve page dimensions (in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Define triangle size
                double triangleWidth = 2.0; // inches
                double triangleHeight = Math.Sqrt(3) / 2 * triangleWidth; // equilateral triangle height

                // Calculate center of the page
                double centerX = pageWidth / 2.0;
                double centerY = pageHeight / 2.0;

                // Calculate triangle vertices so that the shape is centered
                double p1X = centerX - triangleWidth / 2.0;
                double p1Y = centerY - triangleHeight / 3.0; // lower left
                double p2X = centerX + triangleWidth / 2.0;
                double p2Y = centerY - triangleHeight / 3.0; // lower right
                double p3X = centerX;
                double p3Y = centerY + (2.0 * triangleHeight) / 3.0; // top

                // Draw the triangle using a closed polyline (repeat first point at the end)
                long shapeId = page.DrawPolyline(new double[]
                {
                    p1X, p1Y,
                    p2X, p2Y,
                    p3X, p3Y,
                    p1X, p1Y // close the shape
                });

                // Retrieve the shape to optionally modify its appearance
                Shape triangle = page.Shapes.GetShape(shapeId);
                // Example: set line color to black and fill color to light gray
                triangle.Line.LineColor.Value = "#000000";
                triangle.Fill.FillForegnd.Value = "#D3D3D3";

                // Save the diagram to a VSDX file
                string outputPath = "CenteredTriangle.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Diagram saved to '{outputPath}'.");
            }
        }
    }