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

            // Get the first page (automatically created)
            Page page = diagram.Pages[0];

            // Add 100 rectangle shapes to the page
            for (int i = 0; i < 100; i++)
            {
                // Position shapes in a grid to avoid overlap
                double pinX = (i % 10) * 2.0 + 1.0; // X coordinate
                double pinY = (i / 10) * 2.0 + 1.0; // Y coordinate
                double width = 1.5;
                double height = 1.0;

                // DrawRectangle creates a shape on the page
                page.DrawRectangle(pinX, pinY, width, height);
            }

            // Prepare SVG save options (default settings)
            SVGSaveOptions svgOptions = new SVGSaveOptions();

            // Measure the time taken to export all shapes to individual SVG files
            Stopwatch sw = Stopwatch.StartNew();

            foreach (Shape shape in page.Shapes)
            {
                // Export each shape to a separate SVG file named by its ID
                string outputPath = $"shape_{shape.ID}.svg";
                shape.ToSvg(outputPath, svgOptions);
            }

            sw.Stop();

            // Log the duration
            Console.WriteLine($"Exported {page.Shapes.Count} shapes to SVG in {sw.Elapsed.TotalSeconds:F2} seconds.");
        }
    }