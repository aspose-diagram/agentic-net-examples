using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Expect three arguments: input Visio file, shape ID, output PNG path
            if (args.Length != 3)
            {
                Console.WriteLine("Usage: ExportShapeToPng <inputVisioFile> <shapeId> <outputPngFile>");
                return;
            }

            string inputPath = args[0];
            string shapeIdArg = args[1];
            string outputPath = args[2];

            if (!long.TryParse(shapeIdArg, out long shapeId))
            {
                Console.WriteLine("Invalid shape ID.");
                return;
            }

            // Load the Visio diagram
            Diagram diagram;
            try
            {
                diagram = new Diagram(inputPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load diagram: {ex.Message}");
                return;
            }

            // Assume the shape is on the first page; adjust if needed
            Page page = diagram.Pages[0];
            Shape shape = page.Shapes.GetShape(shapeId);
            if (shape == null)
            {
                Console.WriteLine($"Shape with ID {shapeId} not found on page '{page.Name}'.");
                return;
            }

            // Configure high‑resolution PNG export options
            ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);
            pngOptions.Resolution = 300f; // DPI – increase for higher quality

            // Export the selected shape to PNG
            try
            {
                shape.ToImage(outputPath, pngOptions);
                Console.WriteLine($"Shape {shapeId} exported successfully to '{outputPath}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to export shape: {ex.Message}");
            }
        }
    }