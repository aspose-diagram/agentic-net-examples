using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputFolder>");
            return;
        }

        string inputPath = args[0];
        string outputFolder = args[1];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Guard: ensure the output folder exists or create it.
        if (!Directory.Exists(outputFolder))
        {
            try
            {
                Directory.CreateDirectory(outputFolder);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output folder '{outputFolder}': {ex.Message}");
                return;
            }
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Collect up to 100 non‑deleted shapes from all pages.
            List<Shape> shapesToConvert = new List<Shape>();
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    shapesToConvert.Add(shape);
                    if (shapesToConvert.Count >= 100)
                        break;
                }
                if (shapesToConvert.Count >= 100)
                    break;
            }

            int totalShapes = shapesToConvert.Count;
            if (totalShapes == 0)
            {
                Console.WriteLine("No shapes found to convert.");
                return;
            }

            // Prepare SVG save options (default configuration).
            SVGSaveOptions svgOptions = new SVGSaveOptions();

            // Measure conversion time.
            Stopwatch sw = Stopwatch.StartNew();

            for (int i = 0; i < totalShapes; i++)
            {
                Shape shape = shapesToConvert[i];
                string outputPath = Path.Combine(outputFolder, $"shape_{i + 1}.svg");

                // Convert the shape to SVG.
                shape.ToSvg(outputPath, svgOptions);
            }

            sw.Stop();

            // Log the performance result.
            Console.WriteLine($"Converted {totalShapes} shapes to SVG in {sw.Elapsed.TotalMilliseconds} ms.");
        }
        catch (Exception ex)
        {
            // Log any Aspose or I/O errors.
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}