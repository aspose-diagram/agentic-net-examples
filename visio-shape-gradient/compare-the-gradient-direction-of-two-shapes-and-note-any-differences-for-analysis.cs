using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio diagram file
                string diagramPath = "example.vsdx";

                // IDs of the two shapes to compare (replace with actual IDs)
                long shapeId1 = 1;
                long shapeId2 = 2;

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Access the first page (adjust index if needed)
                Page page = diagram.Pages[0];

                // Retrieve the shapes by ID
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

                // Ensure both shapes have gradient fill enabled
                bool shape1HasGradient = shape1.Fill.GradientFill.GradientEnabled.Value == BOOL.True;
                bool shape2HasGradient = shape2.Fill.GradientFill.GradientEnabled.Value == BOOL.True;

                if (!shape1HasGradient || !shape2HasGradient)
                {
                    Console.WriteLine("One or both shapes do not have gradient fill enabled.");
                    return;
                }

                // Retrieve gradient direction values
                double dir1 = shape1.Fill.GradientFill.GradientDir.Value;
                double dir2 = shape2.Fill.GradientFill.GradientDir.Value;

                // Compare the directions
                if (Math.Abs(dir1 - dir2) < 0.0001)
                {
                    Console.WriteLine("Both shapes have the same gradient direction.");
                }
                else
                {
                    Console.WriteLine($"Gradient direction differs:");
                    Console.WriteLine($" - Shape ID {shapeId1}: Direction = {dir1}");
                    Console.WriteLine($" - Shape ID {shapeId2}: Direction = {dir2}");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }