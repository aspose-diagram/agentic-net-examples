using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Expect: input Visio file, shape ID, output PNG path
            if (args.Length < 3)
            {
                Console.WriteLine("Usage: DiagramShapeExport <inputVisioPath> <shapeId> <outputPngPath>");
                return;
            }

            string inputPath = args[0];
            string shapeIdStr = args[1];
            string outputPath = args[2];

            if (!long.TryParse(shapeIdStr, out long shapeId))
            {
                Console.WriteLine("Invalid shape ID.");
                return;
            }

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram has at least one page
            if (diagram.Pages.Count == 0)
            {
                Console.WriteLine("The diagram contains no pages.");
                return;
            }

            // Access the first page (index 0)
            Page page = diagram.Pages[0];

            // Retrieve the shape by its ID
            Shape shape = page.Shapes.GetShape(shapeId);
            if (shape == null)
            {
                Console.WriteLine($"Shape with ID {shapeId} not found on the first page.");
                return;
            }

            // Set PNG export options with 300 DPI resolution
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
            saveOptions.Resolution = 300; // DPI

            // Export the shape to PNG
            shape.ToImage(outputPath, saveOptions);

            Console.WriteLine($"Shape {shapeId} exported to PNG at '{outputPath}' with 300 DPI.");
        }
    }