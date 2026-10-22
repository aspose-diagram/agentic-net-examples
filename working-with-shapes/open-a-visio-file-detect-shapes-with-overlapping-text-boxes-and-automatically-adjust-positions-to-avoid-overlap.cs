using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths (command‑line arguments or defaults)
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
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Process each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Collect shapes that have a text block (i.e., contain visible text)
                List<Shape> textShapes = new List<Shape>();
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Determine if the shape contains any text
                    string plainText = shape.Text.Value.ToString();
                    if (!string.IsNullOrWhiteSpace(plainText))
                    {
                        textShapes.Add(shape);
                    }
                }

                // Simple O(N^2) overlap detection and resolution
                for (int i = 0; i < textShapes.Count; i++)
                {
                    Shape shapeA = textShapes[i];
                    Rect rectA = GetTextRect(shapeA);

                    for (int j = i + 1; j < textShapes.Count; j++)
                    {
                        Shape shapeB = textShapes[j];
                        Rect rectB = GetTextRect(shapeB);

                        // Check for overlap
                        if (IsOverlapping(rectA, rectB))
                        {
                            // Calculate a horizontal offset to separate the shapes
                            double overlapWidth = rectA.Right - rectB.Left;
                            double offset = overlapWidth + 0.1; // small margin

                            // Move shapeB to the right
                            shapeB.XForm.PinX.Value += offset;

                            // Also move the text block to stay aligned with the shape
                            shapeB.TextXForm.TxtPinX.Value += offset;

                            // Update rectB for subsequent checks
                            rectB = GetTextRect(shapeB);
                        }
                    }
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    // Helper struct to represent a rectangle
    struct Rect
    {
        public double Left;
        public double Right;
        public double Bottom;
        public double Top;
    }

    // Compute the bounding rectangle of a shape's text block
    static Rect GetTextRect(Shape shape)
    {
        Rect rect = new Rect();

        // Use TextXForm for text block geometry
        double pinX = shape.TextXForm.TxtPinX.Value;
        double pinY = shape.TextXForm.TxtPinY.Value;
        double width = shape.TextXForm.TxtWidth.Value;
        double height = shape.TextXForm.TxtHeight.Value;

        rect.Left = pinX - width / 2.0;
        rect.Right = pinX + width / 2.0;
        rect.Bottom = pinY - height / 2.0;
        rect.Top = pinY + height / 2.0;

        return rect;
    }

    // Determine whether two rectangles intersect
    static bool IsOverlapping(Rect a, Rect b)
    {
        return a.Left < b.Right && a.Right > b.Left &&
               a.Bottom < b.Top && a.Top > b.Bottom;
    }
}