using System.IO;
using System;
using System.Diagnostics;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;

class AutoSpaceBenchmark
{
    static void Main()
    {
        try
        {

            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Get the first (default) page
            Page page = diagram.Pages[0];

            // Number of shapes to add for the benchmark
            const int shapeCount = 10000;

            // Add shapes in a grid layout to avoid overlap initially
            double startX = 1.0;
            double startY = 1.0;
            double offsetX = 2.0;
            double offsetY = 2.0;
            int cols = (int)Math.Sqrt(shapeCount);
            int rows = cols;

            for (int i = 0; i < shapeCount; i++)
            {
                int col = i % cols;
                int row = i / cols;
                double pinX = startX + col * offsetX;
                double pinY = startY + row * offsetY;

                // Add a rectangle shape using the built‑in master name "Rectangle"
                // Master ID 0 works for built‑in masters
                diagram.AddShape(pinX, pinY, "Rectangle", 0);
            }

            // Prepare autospace options (default distances)
            AutoSpaceOptions options = new AutoSpaceOptions
            {
                DistanceInHorizontal = 0.3f, // inches
                DistanceInVertical = 0.3f    // inches
            };

            // Warm‑up run to avoid JIT impact
            page.AutoSpaceShapes(page.Shapes, options);

            // Benchmark the AutoSpaceShapes method
            Stopwatch sw = Stopwatch.StartNew();
            page.AutoSpaceShapes(page.Shapes, options);
            sw.Stop();

            Console.WriteLine($"Auto‑spacing {shapeCount} shapes took {sw.ElapsedMilliseconds} ms.");

            // Save the resulting diagram (optional)
            diagram.Save("AutoSpacedDiagram.vdx", SaveFileFormat.Vdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
