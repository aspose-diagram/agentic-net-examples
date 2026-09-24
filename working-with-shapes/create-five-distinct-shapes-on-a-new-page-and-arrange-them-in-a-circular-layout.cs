using System.IO;
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
            // Add a blank page
            diagram.Pages.Add(new Page());

            // Get the first (and only) page
            Page page = diagram.Pages[0];

            // Set page size (10 inches width x 10 inches height)
            page.PageSheet.PageProps.PageWidth.Value = 10.0;
            page.PageSheet.PageProps.PageHeight.Value = 10.0;

            // Center of the page
            double centerX = page.PageSheet.PageProps.PageWidth.Value / 2.0;
            double centerY = page.PageSheet.PageProps.PageHeight.Value / 2.0;
            double radius = 3.0; // radius for circular layout

            // Angles for five shapes (in radians)
            double[] angles = new double[]
            {
                0,
                2 * Math.PI / 5,
                4 * Math.PI / 5,
                6 * Math.PI / 5,
                8 * Math.PI / 5
            };

            // ---------- Rectangle ----------
            double rectX = centerX + radius * Math.Cos(angles[0]);
            double rectY = centerY + radius * Math.Sin(angles[0]);
            long rectId = page.DrawRectangle(rectX, rectY, 1.0, 1.0);
            Shape rectShape = page.Shapes.GetShape(rectId);
            // Optional: set fill color
            rectShape.Fill.FillForegnd.Value = "#FFCCCC";

            // ---------- Ellipse ----------
            double ellipseX = centerX + radius * Math.Cos(angles[1]);
            double ellipseY = centerY + radius * Math.Sin(angles[1]);
            long ellipseId = page.DrawEllipse(ellipseX, ellipseY, 1.0, 1.0);
            Shape ellipseShape = page.Shapes.GetShape(ellipseId);
            ellipseShape.Fill.FillForegnd.Value = "#CCFFCC";

            // ---------- Triangle ----------
            double triX = centerX + radius * Math.Cos(angles[2]);
            double triY = centerY + radius * Math.Sin(angles[2]);
            // Define an equilateral triangle (size ~1)
            long triId = page.DrawPolyline(new double[]
            {
                0, 0,
                1, 0,
                0.5, Math.Sqrt(3) / 2,
                0, 0 // close the shape
            });
            Shape triShape = page.Shapes.GetShape(triId);
            triShape.XForm.PinX.Value = triX;
            triShape.XForm.PinY.Value = triY;
            triShape.Fill.FillForegnd.Value = "#CCCCFF";

            // ---------- Diamond ----------
            double diamondX = centerX + radius * Math.Cos(angles[3]);
            double diamondY = centerY + radius * Math.Sin(angles[3]);
            long diamondId = page.DrawPolyline(new double[]
            {
                0.5, 0,
                1, 0.5,
                0.5, 1,
                0, 0.5,
                0.5, 0 // close the shape
            });
            Shape diamondShape = page.Shapes.GetShape(diamondId);
            diamondShape.XForm.PinX.Value = diamondX;
            diamondShape.XForm.PinY.Value = diamondY;
            diamondShape.Fill.FillForegnd.Value = "#FFFFCC";

            // ---------- Pentagon ----------
            double pentagonX = centerX + radius * Math.Cos(angles[4]);
            double pentagonY = centerY + radius * Math.Sin(angles[4]);
            // Approximate regular pentagon (radius ~0.5)
            double r = 0.5;
            double[] pentagonPoints = new double[12];
            for (int i = 0; i < 5; i++)
            {
                double a = 2 * Math.PI * i / 5 - Math.PI / 2; // start at top
                pentagonPoints[i * 2] = r + r * Math.Cos(a);
                pentagonPoints[i * 2 + 1] = r + r * Math.Sin(a);
            }
            // Close the shape by repeating the first point
            pentagonPoints = new double[]
            {
                pentagonPoints[0], pentagonPoints[1],
                pentagonPoints[2], pentagonPoints[3],
                pentagonPoints[4], pentagonPoints[5],
                pentagonPoints[6], pentagonPoints[7],
                pentagonPoints[8], pentagonPoints[9],
                pentagonPoints[0], pentagonPoints[1]
            };
            long pentagonId = page.DrawPolyline(pentagonPoints);
            Shape pentagonShape = page.Shapes.GetShape(pentagonId);
            pentagonShape.XForm.PinX.Value = pentagonX;
            pentagonShape.XForm.PinY.Value = pentagonY;
            pentagonShape.Fill.FillForegnd.Value = "#FFCCFF";

            // Save the diagram
            diagram.Save("CircularShapes.vsdx", SaveFileFormat.Vsdx);
        }
    }
}
