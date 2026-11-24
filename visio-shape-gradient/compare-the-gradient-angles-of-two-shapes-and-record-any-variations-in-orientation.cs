using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input diagram path, first shape ID, second shape ID
        if (args.Length != 3)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <shapeId1> <shapeId2>");
            return;
        }

        string diagramPath = args[0];
        // Guard: ensure the diagram file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        // Parse shape IDs
        if (!long.TryParse(args[1], out long shapeId1))
        {
            Console.Error.WriteLine($"Error: Invalid shape ID '{args[1]}'");
            return;
        }
        if (!long.TryParse(args[2], out long shapeId2))
        {
            Console.Error.WriteLine($"Error: Invalid shape ID '{args[2]}'");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Assume comparison on the first page; adjust if needed
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("Error: Diagram contains no pages.");
                    return;
                }

                // Retrieve the first page
                Page page = diagram.Pages[0];

                // Retrieve shapes by their IDs
                Shape shape1 = page.Shapes.GetShape(shapeId1);
                Shape shape2 = page.Shapes.GetShape(shapeId2);

                if (shape1 == null)
                {
                    Console.Error.WriteLine($"Error: Shape with ID {shapeId1} not found.");
                    return;
                }
                if (shape2 == null)
                {
                    Console.Error.WriteLine($"Error: Shape with ID {shapeId2} not found.");
                    return;
                }

                // Helper function to obtain gradient angle; returns NaN if not applicable
                double GetGradientAngle(Shape shape)
                {
                    // Ensure gradient fill is enabled; otherwise treat as not set
                    if (shape.Fill?.GradientFill != null &&
                        shape.Fill.GradientFill.GradientEnabled != null &&
                        shape.Fill.GradientFill.GradientEnabled.Value == BOOL.True)
                    {
                        // GradientAngle is a DoubleValue; retrieve its numeric value
                        return shape.Fill.GradientFill.GradientAngle.Value;
                    }
                    // Gradient not defined
                    return double.NaN;
                }

                double angle1 = GetGradientAngle(shape1);
                double angle2 = GetGradientAngle(shape2);

                // Output the retrieved angles
                Console.WriteLine($"Shape {shapeId1} gradient angle: {(double.IsNaN(angle1) ? "N/A" : angle1.ToString())} degrees");
                Console.WriteLine($"Shape {shapeId2} gradient angle: {(double.IsNaN(angle2) ? "N/A" : angle2.ToString())} degrees");

                // Compare angles only if both are defined
                if (double.IsNaN(angle1) || double.IsNaN(angle2))
                {
                    Console.WriteLine("Cannot compare angles because one or both shapes do not have a gradient fill.");
                }
                else if (angle1 == angle2)
                {
                    Console.WriteLine("No variation detected: both shapes have the same gradient angle.");
                }
                else
                {
                    double diff = Math.Abs(angle1 - angle2);
                    Console.WriteLine($"Variation detected: gradient angles differ by {diff} degrees.");
                }
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose or runtime exceptions and report them
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}