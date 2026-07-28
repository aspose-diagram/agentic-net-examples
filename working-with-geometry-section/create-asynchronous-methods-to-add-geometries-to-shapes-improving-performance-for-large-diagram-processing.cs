using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramGeometryAsync
{
    // Helper class containing asynchronous geometry operations
    public static class GeometryHelper
    {
        // Adds a polygon geometry to a shape on a specific page.
        // points: flat array of coordinates [x1, y1, x2, y2, ...]
        public static async Task AddPolygonGeometryAsync(Diagram diagram, int pageIndex, long shapeId, double[] points)
        {
            // Validate input array
            if (points == null || points.Length < 4 || points.Length % 2 != 0)
                throw new ArgumentException("Points array must contain an even number of coordinates (at least two points).");

            await Task.Run(() =>
            {
                // Retrieve the target page
                Page page = diagram.Pages[pageIndex];

                // Retrieve the target shape by its ID
                Shape shape = page.Shapes.GetShape(shapeId);

                // Create a new geometry section
                Geom geom = new Geom();

                // Starting point (MoveTo)
                MoveTo moveTo = new MoveTo { X = { Value = points[0] }, Y = { Value = points[1] } };
                geom.CoordinateCol.Add(moveTo);

                // Add line segments (LineTo) for each subsequent point
                for (int i = 2; i < points.Length; i += 2)
                {
                    LineTo lineTo = new LineTo { X = { Value = points[i] }, Y = { Value = points[i + 1] } };
                    geom.CoordinateCol.Add(lineTo);
                }

                // Optionally close the polygon by returning to the first point
                LineTo closeLine = new LineTo { X = { Value = points[0] }, Y = { Value = points[1] } };
                geom.CoordinateCol.Add(closeLine);

                // Append the geometry to the shape
                shape.Geoms.Add(geom);
            });
        }

        // Adds rectangle geometry to a shape using width and height
        public static async Task AddRectangleGeometryAsync(Diagram diagram, int pageIndex, long shapeId, double width, double height)
        {
            await Task.Run(() =>
            {
                // Retrieve page and shape
                Page page = diagram.Pages[pageIndex];
                Shape shape = page.Shapes.GetShape(shapeId);

                // Define rectangle corners relative to shape's center (PinX/PinY)
                double halfW = width / 2.0;
                double halfH = height / 2.0;

                double[] rectPoints = new double[]
                {
                    -halfW, -halfH,   // top-left
                    halfW, -halfH,    // top-right
                    halfW, halfH,     // bottom-right
                    -halfW, halfH,    // bottom-left
                    -halfW, -halfH    // close back to start
                };

                Geom geom = new Geom();

                // MoveTo first corner
                MoveTo moveTo = new MoveTo { X = { Value = rectPoints[0] }, Y = { Value = rectPoints[1] } };
                geom.CoordinateCol.Add(moveTo);

                // Add remaining corners as LineTo
                for (int i = 2; i < rectPoints.Length; i += 2)
                {
                    LineTo lineTo = new LineTo { X = { Value = rectPoints[i] }, Y = { Value = rectPoints[i + 1] } };
                    geom.CoordinateCol.Add(lineTo);
                }

                // Append geometry to the shape
                shape.Geoms.Add(geom);
            });
        }
    }

    // Main program entry point
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // Path to the source Visio file (must exist)
            string inputPath = "input.vsdx";

            // Guard against missing input file
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                return;
            }

            Diagram diagram;
            try
            {
                // Load the diagram
                diagram = new Diagram(inputPath);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to load diagram: {ex.Message}");
                return;
            }

            // Ensure there is at least one page
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Use the first page for modifications
            Page firstPage = diagram.Pages[0];

            // Add a new rectangle shape (using master name and calculate flag)
            long rectShapeId = firstPage.AddShape(2.0, 2.0, "Rectangle", false);

            // Asynchronously add custom rectangle geometry to the newly created shape
            await GeometryHelper.AddRectangleGeometryAsync(diagram, 0, rectShapeId, 1.5, 1.0);

            // Add a placeholder shape for a custom polygon (using an existing master)
            long polyShapeId = firstPage.AddShape(5.0, 5.0, "Ellipse", false);

            // Define triangle points
            double[] trianglePoints = new double[]
            {
                0.0, 0.0,      // point A
                1.0, 0.0,      // point B
                0.5, 0.866,    // point C (equilateral triangle)
                0.0, 0.0       // close back to A
            };

            // Asynchronously add polygon geometry to the placeholder shape
            await GeometryHelper.AddPolygonGeometryAsync(diagram, 0, polyShapeId, trianglePoints);

            // Save the modified diagram
            await Task.Run(() =>
            {
                string outputPath = "output.vsdx";
                try
                {
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Failed to save diagram: {ex.Message}");
                }
            });
        }
    }
}