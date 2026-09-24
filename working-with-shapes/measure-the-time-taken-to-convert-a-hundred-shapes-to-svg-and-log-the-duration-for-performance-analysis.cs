using System;
using System.Diagnostics;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (replace with your actual file path)
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Get the first page of the diagram
            Page page = diagram.Pages[0];

            // Prepare SVG save options (default options are sufficient)
            SVGSaveOptions svgOptions = new SVGSaveOptions();

            // Directory to store exported SVG files
            string outputDir = "ExportedSvg";
            Directory.CreateDirectory(outputDir);

            // Measure the time taken to export up to 100 shapes to SVG
            Stopwatch stopwatch = Stopwatch.StartNew();

            int exportedCount = 0;
            foreach (Shape shape in page.Shapes)
            {
                if (exportedCount >= 100)
                    break;

                // Build a unique file name for each shape
                string svgPath = Path.Combine(outputDir, $"shape_{shape.ID}.svg");

                // Export the shape to SVG
                shape.ToSvg(svgPath, svgOptions);

                exportedCount++;
            }

            stopwatch.Stop();

            // Log the duration
            Console.WriteLine($"Exported {exportedCount} shapes to SVG in {stopwatch.Elapsed.TotalSeconds:F2} seconds.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
