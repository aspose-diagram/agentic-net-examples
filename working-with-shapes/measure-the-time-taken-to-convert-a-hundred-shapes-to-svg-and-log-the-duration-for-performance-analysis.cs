using System;
using System.Diagnostics;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class ShapeToSvgPerformance
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (replace with actual file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Prepare SVG save options (customize if needed)
            SVGSaveOptions svgOptions = new SVGSaveOptions
            {
                // Example: export hidden pages as well
                ExportHiddenPage = true
            };

            // Collect up to 100 shapes from the diagram (across all pages)
            var shapesToConvert = new System.Collections.Generic.List<Shape>();
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    shapesToConvert.Add(shape);
                    if (shapesToConvert.Count >= 100)
                        break;
                }
                if (shapesToConvert.Count >= 100)
                    break;
            }

            // Ensure we have shapes to process
            if (shapesToConvert.Count == 0)
            {
                Console.WriteLine("No shapes found in the diagram.");
                return;
            }

            // Measure the conversion time
            Stopwatch stopwatch = Stopwatch.StartNew();

            for (int i = 0; i < shapesToConvert.Count; i++)
            {
                Shape shape = shapesToConvert[i];
                // Generate a unique SVG file name for each shape
                string svgFileName = Path.Combine("OutputSvgs", $"Shape_{i + 1}.svg");

                // Ensure the output directory exists
                Directory.CreateDirectory(Path.GetDirectoryName(svgFileName));

                // Convert the shape to SVG using the provided ToSvg method
                shape.ToSvg(svgFileName, svgOptions);
            }

            stopwatch.Stop();

            // Log the duration
            Console.WriteLine($"Converted {shapesToConvert.Count} shapes to SVG in {stopwatch.Elapsed.TotalSeconds:F2} seconds.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
