using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Run the asynchronous processing synchronously for console entry point
                ProcessDiagramAsync(inputPath, outputPath).GetAwaiter().GetResult();

                Console.WriteLine("Diagram processing completed.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        // Asynchronous wrapper that loads the diagram, processes shapes, and saves the result
        private static async Task ProcessDiagramAsync(string inputPath, string outputPath)
        {
            // Load the diagram from file
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Assume processing the first page; adjust as needed
                Page page = diagram.Pages[0];

                // Prepare a list to hold all geometry addition tasks
                List<Task> geometryTasks = new List<Task>();

                // Iterate over each shape on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Example geometry: a simple triangle
                    var points = new List<(double X, double Y)>
                    {
                        (shape.XForm.PinX.Value, shape.XForm.PinY.Value),          // Starting point at shape's current position
                        (shape.XForm.PinX.Value + 1.0, shape.XForm.PinY.Value),    // Right
                        (shape.XForm.PinX.Value + 0.5, shape.XForm.PinY.Value + 1.0) // Top
                    };

                    // Queue the geometry addition without blocking the loop
                    geometryTasks.Add(AddGeometryAsync(page, shape.ID, points));
                }

                // Await completion of all geometry additions
                await Task.WhenAll(geometryTasks);

                // Save the modified diagram using a valid SaveFileFormat enum value
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }

        // Asynchronously adds a custom geometry (polyline) to a shape identified by shapeId
        private static async Task AddGeometryAsync(Page page, long shapeId, List<(double X, double Y)> points)
        {
            // Offload the geometry creation to a background thread to avoid blocking
            await Task.Run(() =>
            {
                // Retrieve the shape instance from the page
                Shape shape = page.Shapes.GetShape(shapeId);

                // Create a new Geom object which will hold the coordinate collection
                Geom geom = new Geom();

                // Ensure there is at least one point to define the geometry
                if (points == null || points.Count == 0)
                    return;

                // Add a MoveTo segment for the first point (starting position)
                MoveTo move = new MoveTo();
                move.X.Value = points[0].X;
                move.Y.Value = points[0].Y;
                geom.CoordinateCol.Add(move);

                // Add LineTo segments for each subsequent point
                for (int i = 1; i < points.Count; i++)
                {
                    LineTo line = new LineTo();
                    line.X.Value = points[i].X;
                    line.Y.Value = points[i].Y;
                    geom.CoordinateCol.Add(line);
                }

                // Optionally close the shape by returning to the first point
                // Uncomment the following lines if a closed polygon is desired
                //LineTo close = new LineTo();
                //close.X.Value = points[0].X;
                //close.Y.Value = points[0].Y;
                //geom.CoordinateCol.Add(close);

                // Append the new geometry to the shape's Geoms collection
                shape.Geoms.Add(geom);
            });
        }
    }