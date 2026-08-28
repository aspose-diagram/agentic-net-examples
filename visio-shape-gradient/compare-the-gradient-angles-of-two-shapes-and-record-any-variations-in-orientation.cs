using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio diagram file
                string diagramPath = "input.vsdx";

                // Load the diagram
                using (Diagram diagram = new Diagram(diagramPath))
                {
                    // Assume we are working with the first page
                    Page page = diagram.Pages[0];

                    // IDs of the two shapes to compare (replace with actual IDs)
                    long shapeId1 = 1;
                    long shapeId2 = 2;

                    // Retrieve the shapes by ID
                    Shape shape1 = page.Shapes.GetShape(shapeId1);
                    Shape shape2 = page.Shapes.GetShape(shapeId2);

                    if (shape1 == null)
                        throw new Exception($"Shape with ID {shapeId1} not found.");
                    if (shape2 == null)
                        throw new Exception($"Shape with ID {shapeId2} not found.");

                    // Access gradient angle values (in degrees)
                    double angle1 = shape1.Fill.GradientFill.GradientAngle.Value;
                    double angle2 = shape2.Fill.GradientFill.GradientAngle.Value;

                    // Compare the angles
                    if (angle1 == angle2)
                    {
                        Console.WriteLine($"Both shapes have the same gradient angle: {angle1} degrees.");
                    }
                    else
                    {
                        double difference = Math.Abs(angle1 - angle2);
                        Console.WriteLine($"Gradient angles differ. Shape {shapeId1}: {angle1}°, Shape {shapeId2}: {angle2}°.");
                        Console.WriteLine($"Difference: {difference}°.");
                    }
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }