using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
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

        // Ensure the output directory exists (create if necessary).
        if (!Directory.Exists(outputFolder))
        {
            try
            {
                Directory.CreateDirectory(outputFolder);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output directory: {ex.Message}");
                return;
            }
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Collect up to 100 non‑deleted shapes from the diagram.
            List<Shape> shapesToConvert = new List<Shape>();
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes.
                    if (shape.Del == BOOL.False)
                    {
                        shapesToConvert.Add(shape);
                        if (shapesToConvert.Count >= 100)
                            break;
                    }
                }
                if (shapesToConvert.Count >= 100)
                    break;
            }

            int shapeCount = shapesToConvert.Count;
            if (shapeCount == 0)
            {
                Console.WriteLine("No shapes found to convert.");
                return;
            }

            // Measure the time taken to convert the collected shapes to SVG.
            Stopwatch sw = Stopwatch.StartNew();

            foreach (Shape shape in shapesToConvert)
            {
                // Build a unique file name for each shape's SVG output.
                string svgPath = Path.Combine(outputFolder, $"shape_{shape.ID}.svg");

                // Convert the shape to SVG using default SVGSaveOptions.
                shape.ToSvg(svgPath, new SVGSaveOptions());
            }

            sw.Stop();

            // Log the duration and number of processed shapes.
            Console.WriteLine($"Converted {shapeCount} shape(s) to SVG in {sw.Elapsed.TotalMilliseconds} ms.");
        }
        catch (Exception ex)
        {
            // Log any Aspose or I/O errors.
            Console.Error.WriteLine($"Error during processing: {ex.Message}");
        }
    }
}