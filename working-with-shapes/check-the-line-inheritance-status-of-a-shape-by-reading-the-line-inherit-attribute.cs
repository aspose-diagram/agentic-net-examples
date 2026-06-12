using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Expect two arguments: input Visio file path and shape ID to inspect
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: LineInheritanceCheck <inputVisioFile> <shapeId>");
                return;
            }

            string inputPath = args[0];
            if (!long.TryParse(args[1], out long shapeId))
            {
                Console.WriteLine("Invalid shape ID. It must be a numeric value.");
                return;
            }

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Access the first page (or you can iterate pages to find the shape)
            Page page = diagram.Pages[0];

            // Retrieve the shape by its ID
            Shape shape = page.Shapes.GetShape(shapeId);
            if (shape == null)
            {
                Console.WriteLine($"Shape with ID {shapeId} not found on page {page.Name}.");
                return;
            }

            // Determine if line properties are inherited.
            // Compare a representative line property (e.g., LineColor) with its inherited counterpart.
            bool isLineColorInherited = shape.Line.LineColor.Value == shape.InheritLine.LineColor.Value;
            bool isLineWeightInherited = shape.Line.LineWeight.Value == shape.InheritLine.LineWeight.Value;

            // If both key properties match, we consider the line to be inherited.
            bool isLineInherited = isLineColorInherited && isLineWeightInherited;

            Console.WriteLine($"Shape ID: {shapeId}");
            Console.WriteLine($"Line Color Inherited: {isLineColorInherited}");
            Console.WriteLine($"Line Weight Inherited: {isLineWeightInherited}");
            Console.WriteLine($"Overall Line Inheritance Status: {(isLineInherited ? "Inherited" : "Not Inherited")}");
        }
    }