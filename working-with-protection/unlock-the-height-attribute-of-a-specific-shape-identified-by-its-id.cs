using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Expect three arguments: input file path, shape ID, output file path
            if (args.Length != 3)
            {
                Console.WriteLine("Usage: UnlockShapeHeight <input.vsdx> <shapeId> <output.vsdx>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[2];

            // Parse the shape ID (must be a long integer)
            if (!long.TryParse(args[1], out long shapeId))
            {
                Console.WriteLine("Invalid shape ID. It must be a numeric value.");
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
            if (diagram.Pages.Count == 0)
            {
                Console.WriteLine("The diagram contains no pages.");
                return;
            }

            Page page = diagram.Pages[0];

            // Retrieve the shape by its ID
            Shape shape;
            try
            {
                shape = page.Shapes.GetShape(shapeId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Shape with ID {shapeId} not found: {ex.Message}");
                return;
            }

            // Unlock the height attribute by clearing the LockHeight protection flag
            // The LockHeight property is read‑only; modify its underlying BOOL value.
            shape.Protection.LockHeight.Value = BOOL.False;

            // Save the modified diagram
            try
            {
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to '{outputPath}'. Height attribute unlocked for shape ID {shapeId}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to save diagram: {ex.Message}");
            }
        }
    }