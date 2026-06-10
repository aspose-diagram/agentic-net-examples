using System.IO;
using System;
using System.Diagnostics;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load a template diagram that contains at least one master shape.
            // Replace "template.vsdx" with the path to an existing Visio file.
            Diagram diagram = new Diagram("template.vsdx");

            // Use the first master shape from the template for adding new shapes.
            Master master = diagram.Masters[0];

            // Number of shapes to add for the benchmark.
            int shapeCount = 5000;

            // Add shapes in a simple grid layout.
            for (int i = 0; i < shapeCount; i++)
            {
                double x = (i % 100) * 1.0;          // Horizontal position
                double y = (i / 100) * 1.0;          // Vertical position
                diagram.AddShape(x, y, master.NameU, master.ID);
            }

            // Get the active page that now contains the added shapes.
            Page page = diagram.ActivePage;

            // Configure auto‑spacing options.
            AutoSpaceOptions options = new AutoSpaceOptions
            {
                DistanceInHorizontal = 0.5f, // inches
                DistanceInVertical = 0.5f    // inches
            };

            // Measure the time taken by AutoSpaceShapes.
            Stopwatch sw = Stopwatch.StartNew();
            page.AutoSpaceShapes(page.Shapes, options);
            sw.Stop();

            Console.WriteLine($"AutoSpaceShapes for {shapeCount} shapes took {sw.ElapsedMilliseconds} ms");

            // Save the resulting diagram.
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
