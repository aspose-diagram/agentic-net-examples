using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Validate input arguments
            if (args.Length < 3)
            {
                Console.WriteLine("Usage: GradientComparison <diagramPath> <shapeName1> <shapeName2>");
                return;
            }

            string diagramPath = args[0];
            string shapeName1 = args[1];
            string shapeName2 = args[2];

            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Access the first page (adjust if needed)
            Page page = diagram.Pages[0];

            // Locate the two shapes by their universal names (NameU)
            Shape shape1 = null;
            Shape shape2 = null;

            foreach (Shape shape in page.Shapes)
            {
                if (shape.NameU != null && shape.NameU.Equals(shapeName1, StringComparison.OrdinalIgnoreCase))
                {
                    shape1 = shape;
                }
                else if (shape.NameU != null && shape.NameU.Equals(shapeName2, StringComparison.OrdinalIgnoreCase))
                {
                    shape2 = shape;
                }

                if (shape1 != null && shape2 != null)
                    break;
            }

            // Ensure both shapes were found
            if (shape1 == null)
                throw new Exception($"Shape \"{shapeName1}\" not found on the first page.");
            if (shape2 == null)
                throw new Exception($"Shape \"{shapeName2}\" not found on the first page.");

            // Retrieve gradient direction values
            // GradientDir is a DoubleValue; its .Value holds the direction index (0‑7)
            double dir1 = shape1.Fill.GradientFill.GradientDir.Value;
            double dir2 = shape2.Fill.GradientFill.GradientDir.Value;

            // Output the comparison result
            if (dir1 == dir2)
            {
                Console.WriteLine($"Both shapes have the same gradient direction: {dir1}");
            }
            else
            {
                Console.WriteLine($"Gradient direction differs:");
                Console.WriteLine($" - {shapeName1}: {dir1}");
                Console.WriteLine($" - {shapeName2}: {dir2}");
            }
        }
    }