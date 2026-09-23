using System.IO;
using System;
using System.Diagnostics;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.AutoLayout;

class Program
{
    static void Main()
    {
        // Number of shapes to create for the benchmark
        const int shapeCount = 5000;

        // Create a new empty diagram
        Diagram diagram = new Diagram();

        // Add a new page to the diagram
        Page page = new Page();
        diagram.Pages.Add(page);

        // Measure time taken to add shapes
        Stopwatch swAdd = Stopwatch.StartNew();
        for (int i = 0; i < shapeCount; i++)
        {
            // Simple grid layout for shape placement
            double pinX = (i % 100) * 0.5 + 1.0; // 0.5 inch spacing, offset by 1 inch
            double pinY = (i / 100) * 0.5 + 1.0;
            double width = 0.4;
            double height = 0.3;

            // Draw a rectangle shape; returns the shape ID (long)
            page.DrawRectangle(pinX, pinY, width, height);
        }
        swAdd.Stop();
        Console.WriteLine($"Added {shapeCount} shapes in {swAdd.ElapsedMilliseconds} ms.");

        // Prepare auto‑spacing options
        AutoSpaceOptions autoSpaceOpts = new AutoSpaceOptions
        {
            DistanceInHorizontal = 0.2, // inches
            DistanceInVertical = 0.2    // inches
        };

        // Measure time taken for auto‑spacing
        Stopwatch swAutoSpace = Stopwatch.StartNew();
        page.AutoSpaceShapes(page.Shapes, autoSpaceOpts);
        swAutoSpace.Stop();
        Console.WriteLine($"Auto‑spacing completed in {swAutoSpace.ElapsedMilliseconds} ms.");

        // Save the diagram for visual verification (optional)
        diagram.Save("AutoSpaceBenchmark.vsdx", SaveFileFormat.Vsdx);
        Console.WriteLine("Diagram saved as AutoSpaceBenchmark.vsdx");
    }
}
