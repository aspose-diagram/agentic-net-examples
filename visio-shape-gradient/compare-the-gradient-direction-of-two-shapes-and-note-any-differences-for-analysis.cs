using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Access the first page (adjust index if needed)
                Page page = diagram.Pages[0];

                // Retrieve the two shapes to compare (replace with actual IDs or logic)
                long shapeId1 = 1; // example shape ID
                long shapeId2 = 2; // example shape ID

                Shape shape1 = page.Shapes.GetShape(shapeId1);
                Shape shape2 = page.Shapes.GetShape(shapeId2);

                if (shape1 == null || shape2 == null)
                {
                    Console.WriteLine("One or both shapes could not be found.");
                    return;
                }

                // Ensure gradient fill is enabled for both shapes
                bool gradientEnabled1 = shape1.Fill.GradientFill.GradientEnabled.Value == BOOL.True;
                bool gradientEnabled2 = shape2.Fill.GradientFill.GradientEnabled.Value == BOOL.True;

                if (!gradientEnabled1 || !gradientEnabled2)
                {
                    Console.WriteLine("Gradient fill is not enabled on one or both shapes.");
                    return;
                }

                // Retrieve gradient direction values (0‑7 where each value represents a direction)
                double dir1 = shape1.Fill.GradientFill.GradientDir.Value;
                double dir2 = shape2.Fill.GradientFill.GradientDir.Value;

                Console.WriteLine($"Shape {shapeId1} gradient direction: {dir1}");
                Console.WriteLine($"Shape {shapeId2} gradient direction: {dir2}");

                // Compare the directions and report differences
                if (dir1 == dir2)
                {
                    Console.WriteLine("Both shapes have the same gradient direction.");
                }
                else
                {
                    Console.WriteLine("Gradient directions differ between the two shapes.");
                    Console.WriteLine($"Difference: {Math.Abs(dir1 - dir2)} (direction units)");
                }

                // Save the diagram (unchanged) to demonstrate lifecycle usage
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }