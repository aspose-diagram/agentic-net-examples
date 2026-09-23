using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.Diagram;

namespace DiagramGeometryAsync
{
    // Helper class for geometry operations
    public static class GeometryHelper
    {
        // Adds a polyline geometry to the specified shape.
        // points: flat array of coordinates [x1, y1, x2, y2, ...]
        public static Task AddPolylineAsync(Shape shape, double[] points)
        {
            if (shape == null) throw new ArgumentNullException(nameof(shape));
            if (points == null) throw new ArgumentNullException(nameof(points));
            if (points.Length < 4 || points.Length % 2 != 0)
                throw new ArgumentException("Points array must contain an even number of values (at least two points).");

            // Geometry creation can be CPU‑bound; run it on a background thread.
            return Task.Run(() =>
            {
                // Create a new geometry section for the shape.
                Geom geom = new Geom();
                shape.Geoms.Add(geom);

                // First point – MoveTo
                MoveTo move = new MoveTo
                {
                    X = { Value = points[0] },
                    Y = { Value = points[1] }
                };
                geom.CoordinateCol.Add(move);

                // Remaining points – LineTo
                for (int i = 2; i < points.Length; i += 2)
                {
                    LineTo line = new LineTo
                    {
                        X = { Value = points[i] },
                        Y = { Value = points[i + 1] }
                    };
                    geom.CoordinateCol.Add(line);
                }
            });
        }
    }

    // Processor that handles bulk geometry additions asynchronously
    public class DiagramProcessor
    {
        private readonly Diagram _diagram;

        public DiagramProcessor(Diagram diagram)
        {
            _diagram = diagram ?? throw new ArgumentNullException(nameof(diagram));
        }

        // Adds the same polyline geometry to a collection of shape IDs on a given page.
        public async Task AddGeometryToShapesAsync(int pageIndex, IEnumerable<long> shapeIds, double[] polylinePoints)
        {
            if (shapeIds == null) throw new ArgumentNullException(nameof(shapeIds));
            if (polylinePoints == null) throw new ArgumentNullException(nameof(polylinePoints));

            Page page = _diagram.Pages[pageIndex];
            var tasks = new List<Task>();

            foreach (long shapeId in shapeIds)
            {
                // Retrieve the shape instance.
                Shape shape = page.Shapes.GetShape(shapeId);
                if (shape == null) continue; // Skip missing shapes.

                // Queue geometry addition.
                tasks.Add(GeometryHelper.AddPolylineAsync(shape, polylinePoints));
            }

            // Await all geometry operations.
            await Task.WhenAll(tasks);
        }
    }

    class Program
    {
        // Entry point – async Main is supported in .NET 8.0 console apps.
        static async Task Main(string[] args)
        {
            try
            {

                // Example file paths – replace with actual locations.
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the diagram.
                Diagram diagram = new Diagram(inputPath);

                // Prepare a list of shape IDs to which we want to add geometry.
                // In a real scenario, populate this list based on your criteria.
                List<long> targetShapeIds = new List<long>();
                Page firstPage = diagram.Pages[0];
                foreach (Shape shape in firstPage.Shapes)
                {
                    // Example filter: only non‑deleted shapes.
                    if (shape.Del == BOOL.False)
                    {
                        targetShapeIds.Add(shape.ID);
                    }
                }

                // Define a simple triangle polyline (closed shape).
                double[] trianglePoints = new double[]
                {
                    1.0, 1.0,   // Point A
                    3.0, 1.0,   // Point B
                    2.0, 3.0,   // Point C
                    1.0, 1.0    // Close back to A
                };

                // Process geometry additions asynchronously.
                DiagramProcessor processor = new DiagramProcessor(diagram);
                await processor.AddGeometryToShapesAsync(0, targetShapeIds, trianglePoints);

                // Save the modified diagram.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}