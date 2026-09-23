using System.IO;
using System;
using System.Diagnostics;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Create a new empty diagram
        Diagram diagram = new Diagram();

        // Get the first (and only) page
        Page page = diagram.Pages[0];

        // Prepare a stopwatch for benchmarking
        Stopwatch sw = new Stopwatch();
        sw.Start();

        // Add 100 rectangle shapes
        double startX = 1.0;
        double startY = 1.0;
        double width = 2.0;
        double height = 1.0;
        double offset = 0.5; // spacing between rectangles

        for (int i = 0; i < 100; i++)
        {
            // Position each rectangle slightly offset to avoid overlap
            double pinX = startX + (i % 10) * (width + offset);
            double pinY = startY + (i / 10) * (height + offset);

            // DrawRectangle returns the shape ID (long)
            long shapeId = page.DrawRectangle(pinX, pinY, width, height);
            // Shape can be retrieved if needed:
            // Shape shape = page.Shapes.GetShape(shapeId);
        }

        sw.Stop();

        // Log the elapsed time in milliseconds
        Console.WriteLine($"Time to add 100 rectangles: {sw.ElapsedMilliseconds} ms");

        // Optionally save the diagram to verify the shapes were added
        diagram.Save("BenchmarkResult.vsdx", SaveFileFormat.Vsdx);
    }
}
