using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths (use defaults if not provided)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block to ensure proper disposal
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Process each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Collect all non-deleted shapes on the page
                    List<Shape> shapes = new List<Shape>();
                    foreach (Shape shape in page.Shapes)
                    {
                        if (shape.Del == BOOL.False)
                        {
                            shapes.Add(shape);
                        }
                    }

                    // Simple O(n^2) overlap detection and resolution
                    for (int i = 0; i < shapes.Count; i++)
                    {
                        Shape s1 = shapes[i];
                        Rectangle rect1 = GetBoundingRectangle(s1);

                        for (int j = i + 1; j < shapes.Count; j++)
                        {
                            Shape s2 = shapes[j];
                            Rectangle rect2 = GetBoundingRectangle(s2);

                            if (RectanglesOverlap(rect1, rect2))
                            {
                                // Calculate horizontal offset to separate the shapes
                                double offset = (rect1.Right - rect2.Left) + 0.5; // add a small gap
                                double newPinX = s2.XForm.PinX.Value + offset;

                                // Reposition the second shape
                                s2.XForm.PinX.Value = newPinX;

                                // Update rectangle for further checks
                                rect2 = GetBoundingRectangle(s2);
                            }
                        }
                    }
                }

                // Save the modified diagram to the output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    // Helper struct to represent a shape's bounding box
    private struct Rectangle
    {
        public double Left;
        public double Right;
        public double Top;
        public double Bottom;
    }

    // Compute the bounding rectangle of a shape based on its PinX, PinY, Width, and Height
    private static Rectangle GetBoundingRectangle(Shape shape)
    {
        double pinX = shape.XForm.PinX.Value;
        double pinY = shape.XForm.PinY.Value;
        double width = shape.XForm.Width.Value;
        double height = shape.XForm.Height.Value;

        Rectangle rect = new Rectangle
        {
            Left = pinX - width / 2.0,
            Right = pinX + width / 2.0,
            Bottom = pinY - height / 2.0,
            Top = pinY + height / 2.0
        };
        return rect;
    }

    // Determine if two rectangles intersect
    private static bool RectanglesOverlap(Rectangle r1, Rectangle r2)
    {
        bool horizontalOverlap = r1.Left < r2.Right && r1.Right > r2.Left;
        bool verticalOverlap = r1.Bottom < r2.Top && r1.Top > r2.Bottom;
        return horizontalOverlap && verticalOverlap;
    }
}