using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;

class VisioOverlapResolver
{
    // Represents a simple rectangle for overlap detection
    private struct Rect
    {
        public double Left;
        public double Right;
        public double Top;
        public double Bottom;
    }

    // Compute the bounding rectangle of a shape based on its geometry
    private static Rect GetShapeRect(Shape shape)
    {
        // PinX/Y are the center of the shape
        double pinX = shape.XForm.PinX.Value;
        double pinY = shape.XForm.PinY.Value;
        double width = shape.XForm.Width.Value;
        double height = shape.XForm.Height.Value;

        double left = pinX - width / 2.0;
        double right = pinX + width / 2.0;
        double top = pinY + height / 2.0;
        double bottom = pinY - height / 2.0;

        return new Rect { Left = left, Right = right, Top = top, Bottom = bottom };
    }

    // Simple rectangle overlap test
    private static bool Overlaps(Rect a, Rect b)
    {
        return a.Left < b.Right && a.Right > b.Left && a.Bottom < b.Top && a.Top > b.Bottom;
    }

    // Resolve overlaps on a single page by nudging overlapping shapes to the right
    private static void ResolvePageOverlaps(Page page)
    {
        // Collect shapes that have geometry (skip connectors, groups, etc.)
        List<Shape> shapes = new List<Shape>();
        foreach (Shape shape in page.Shapes)
        {
            // Only consider shapes with a visible geometry (Width/Height > 0)
            if (shape.XForm.Width.Value > 0 && shape.XForm.Height.Value > 0)
                shapes.Add(shape);
        }

        // Simple O(n^2) detection and resolution
        for (int i = 0; i < shapes.Count; i++)
        {
            Shape shapeA = shapes[i];
            Rect rectA = GetShapeRect(shapeA);

            for (int j = i + 1; j < shapes.Count; j++)
            {
                Shape shapeB = shapes[j];
                Rect rectB = GetShapeRect(shapeB);

                if (Overlaps(rectA, rectB))
                {
                    // Move shapeB to the right by its width plus a small gap
                    double gap = 0.2; // optional extra spacing
                    double shift = shapeB.XForm.Width.Value + gap;
                    shapeB.XForm.PinX.Value += shift;

                    // Recalculate rectangle for shapeB after moving
                    rectB = GetShapeRect(shapeB);
                    // Update stored rectangle for future checks
                    shapes[j] = shapeB;
                }
            }
        }
    }

    static void Main()
    {
        try
        {

            // Load the Visio diagram (replace with your actual file path)
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Process each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                ResolvePageOverlaps(page);
            }

            // Save the modified diagram (replace with desired output path)
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine("Overlap detection and repositioning completed.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
