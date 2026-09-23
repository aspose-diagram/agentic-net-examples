using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file (adjust as needed)
                string diagramPath = "input.vsdx";

                // Load the diagram from file
                Diagram diagram;
                using (FileStream fs = new FileStream(diagramPath, FileMode.Open, FileAccess.Read))
                {
                    diagram = new Diagram(fs);
                }

                // Find the two shapes by their universal names (NameU)
                Shape shape1 = FindShapeByNameU(diagram, "Shape1");
                Shape shape2 = FindShapeByNameU(diagram, "Shape2");

                if (shape1 == null || shape2 == null)
                {
                    Console.WriteLine("One or both shapes were not found in the diagram.");
                    return;
                }

                // Retrieve gradient angles (in degrees) from each shape
                double angle1 = shape1.Fill.GradientFill.GradientAngle.Value;
                double angle2 = shape2.Fill.GradientFill.GradientAngle.Value;

                // Compare the angles and report any differences
                if (Math.Abs(angle1 - angle2) < 0.0001)
                {
                    Console.WriteLine($"Both shapes have identical gradient angles: {angle1}°");
                }
                else
                {
                    Console.WriteLine("Gradient angle variation detected:");
                    Console.WriteLine($"Shape '{shape1.NameU}' angle: {angle1}°");
                    Console.WriteLine($"Shape '{shape2.NameU}' angle: {angle2}°");
                    Console.WriteLine($"Difference: {Math.Abs(angle1 - angle2)}°");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        // Helper method to locate a shape by its universal name (NameU) across all pages
        static Shape FindShapeByNameU(Diagram diagram, string nameU)
        {
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.NameU == nameU)
                    {
                        return shape;
                    }
                }
            }
            return null;
        }
    }