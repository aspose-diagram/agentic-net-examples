using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Expect three arguments: diagram file path, first shape ID, second shape ID
            if (args.Length != 3)
            {
                Console.WriteLine("Usage: ShapeVerification <diagramPath> <shapeId1> <shapeId2>");
                return;
            }

            string diagramPath = args[0];
            if (!long.TryParse(args[1], out long shapeId1))
            {
                Console.WriteLine("Invalid first shape ID.");
                return;
            }

            if (!long.TryParse(args[2], out long shapeId2))
            {
                Console.WriteLine("Invalid second shape ID.");
                return;
            }

            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Access the first page (adjust if needed)
            Page page = diagram.Pages[0];

            // Retrieve the shapes by their IDs
            Shape shape1 = page.Shapes.GetShape(shapeId1);
            Shape shape2 = page.Shapes.GetShape(shapeId2);

            if (shape1 == null)
            {
                Console.WriteLine($"Shape with ID {shapeId1} not found.");
                return;
            }

            if (shape2 == null)
            {
                Console.WriteLine($"Shape with ID {shapeId2} not found.");
                return;
            }

            // Verify connection
            bool areConnected = shape1.IsConnected(shape2);
            Console.WriteLine($"Shapes connected: {areConnected}");

            // Verify gluing
            bool areGlued = shape1.IsGlued(shape2);
            Console.WriteLine($"Shapes glued: {areGlued}");

            // Throw exception if either verification fails (as per requirement)
            if (!areConnected)
                throw new Exception($"Shapes {shapeId1} and {shapeId2} are NOT connected.");

            if (!areGlued)
                throw new Exception($"Shapes {shapeId1} and {shapeId2} are NOT glued.");

            Console.WriteLine("Both connection and gluing are verified successfully.");
        }
    }