using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            // Prompt for diagram file path
            Console.Write("Enter the path to the Visio diagram file: ");
            string diagramPath = Console.ReadLine();

            // Prompt for first shape ID
            Console.Write("Enter the ID of the first shape to compare: ");
            if (!long.TryParse(Console.ReadLine(), out long shapeId1))
            {
                Console.WriteLine("Invalid shape ID.");
                return;
            }

            // Prompt for second shape ID
            Console.Write("Enter the ID of the second shape to compare: ");
            if (!long.TryParse(Console.ReadLine(), out long shapeId2))
            {
                Console.WriteLine("Invalid shape ID.");
                return;
            }

            // Load the diagram
            Diagram diagram;
            try
            {
                diagram = new Diagram(diagramPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load diagram: {ex.Message}");
                return;
            }

            // Use the first page (index 0) for shape retrieval
            if (diagram.Pages.Count == 0)
            {
                Console.WriteLine("The diagram contains no pages.");
                return;
            }

            Page page = diagram.Pages[0];

            // Retrieve shapes by ID
            Shape shape1;
            Shape shape2;
            try
            {
                shape1 = page.Shapes.GetShape(shapeId1);
                shape2 = page.Shapes.GetShape(shapeId2);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving shapes: {ex.Message}");
                return;
            }

            // Ensure both shapes have gradient fill enabled
            bool shape1HasGradient = shape1.Fill.GradientFill.GradientEnabled.Value == BOOL.True;
            bool shape2HasGradient = shape2.Fill.GradientFill.GradientEnabled.Value == BOOL.True;

            if (!shape1HasGradient && !shape2HasGradient)
            {
                Console.WriteLine("Neither shape has a gradient fill enabled.");
                return;
            }

            if (!shape1HasGradient)
            {
                Console.WriteLine("Shape 1 does not have a gradient fill enabled.");
                return;
            }

            if (!shape2HasGradient)
            {
                Console.WriteLine("Shape 2 does not have a gradient fill enabled.");
                return;
            }

            // Compare gradient direction values
            double dir1 = shape1.Fill.GradientFill.GradientDir.Value;
            double dir2 = shape2.Fill.GradientFill.GradientDir.Value;

            Console.WriteLine($"Shape {shapeId1} gradient direction: {dir1}");
            Console.WriteLine($"Shape {shapeId2} gradient direction: {dir2}");

            if (Math.Abs(dir1 - dir2) < 0.0001)
            {
                Console.WriteLine("Both shapes have the same gradient direction.");
            }
            else
            {
                Console.WriteLine("The shapes have different gradient directions.");
                double difference = Math.Abs(dir1 - dir2);
                Console.WriteLine($"Difference in direction: {difference}");
            }
        }
    }