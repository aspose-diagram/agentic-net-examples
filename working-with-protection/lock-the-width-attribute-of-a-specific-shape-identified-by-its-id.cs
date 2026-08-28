using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Expect three arguments: input diagram path, shape ID, output diagram path
            if (args.Length != 3)
            {
                Console.WriteLine("Usage: DiagramLockWidthExample <inputPath> <shapeId> <outputPath>");
                return;
            }

            string inputPath = args[0];
            string shapeIdArg = args[1];
            string outputPath = args[2];

            if (!long.TryParse(shapeIdArg, out long shapeId))
            {
                Console.WriteLine("Invalid shape ID. It must be a numeric value.");
                return;
            }

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Find the shape by ID on any page
            Shape targetShape = null;
            foreach (Page page in diagram.Pages)
            {
                // GetShape returns null if the ID is not present on this page
                Shape shape = page.Shapes.GetShape(shapeId);
                if (shape != null)
                {
                    targetShape = shape;
                    break;
                }
            }

            if (targetShape == null)
            {
                Console.WriteLine($"Shape with ID {shapeId} not found in the diagram.");
                return;
            }

            // Lock the width attribute of the shape
            targetShape.Protection.LockWidth.Value = BOOL.True;

            // Save the modified diagram (preserving the original format)
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Shape width locked and diagram saved to '{outputPath}'.");
        }
    }