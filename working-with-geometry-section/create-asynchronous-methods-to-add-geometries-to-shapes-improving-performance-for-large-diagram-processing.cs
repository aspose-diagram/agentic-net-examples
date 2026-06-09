using System;
using System.Threading.Tasks;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        // Entry point - async Main is supported in .NET 8.0
        static async Task Main(string[] args)
        {
            try
            {

                // Load an existing Visio diagram (replace with actual path)
                Diagram diagram = new Diagram("input.vsdx");

                // Process all shapes on the first page asynchronously
                await AddGeometryToAllShapesAsync(diagram, diagram.Pages[0]);

                // Save the modified diagram
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        // Asynchronously adds custom geometry to every shape on a given page
        private static async Task AddGeometryToAllShapesAsync(Diagram diagram, Page page)
        {
            // Iterate over shape IDs to avoid collection modification issues
            foreach (Shape shape in page.Shapes)
            {
                // Skip deleted shapes
                if (shape.Del == BOOL.True)
                    continue;

                // Add geometry to each shape on a background thread
                await AddCustomGeometryAsync(shape);
            }
        }

        // Asynchronously adds a simple MoveTo + LineTo geometry to a shape
        private static Task AddCustomGeometryAsync(Shape shape)
        {
            return Task.Run(() =>
            {
                // Ensure the shape has at least one Geom container
                if (shape.Geoms.Count == 0)
                {
                    Geom newGeom = new Geom();
                    shape.Geoms.Add(newGeom);
                }

                // Use the first Geom container
                Geom targetGeom = shape.Geoms[0];

                // Create a MoveTo segment (starting point at 0,0)
                MoveTo move = new MoveTo();
                move.X.Value = 0.0;
                move.Y.Value = 0.0;

                // Create a LineTo segment (draw a line to 1,1)
                LineTo line = new LineTo();
                line.X.Value = 1.0;
                line.Y.Value = 1.0;

                // Append the geometry segments
                targetGeom.CoordinateCol.Add(move);
                targetGeom.CoordinateCol.Add(line);
            });
        }
    }