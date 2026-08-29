using System.IO;
using System;
using System.Diagnostics;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with actual file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Initialize SVG save options (customize if needed)
            SVGSaveOptions svgOptions = new SVGSaveOptions();

            // Start timing the conversion of shapes to SVG
            Stopwatch stopwatch = Stopwatch.StartNew();

            // Determine how many shapes to process (up to 100)
            int shapeCount = Math.Min(100, diagram.Pages[0].Shapes.Count);

            // Convert each shape to an individual SVG file
            for (int i = 0; i < shapeCount; i++)
            {
                Shape shape = diagram.Pages[0].Shapes[i];
                string svgFileName = $"shape_{i + 1}.svg";
                shape.ToSvg(svgFileName, svgOptions);
            }

            // Stop timing
            stopwatch.Stop();

            // Log the duration
            Console.WriteLine($"Converted {shapeCount} shapes to SVG in {stopwatch.ElapsedMilliseconds} ms.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
