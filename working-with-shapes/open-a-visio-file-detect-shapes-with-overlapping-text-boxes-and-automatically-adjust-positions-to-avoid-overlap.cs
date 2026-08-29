using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output Visio file path (second argument or default)
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram has at least one page
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Work with the first page (modify as needed for multi‑page docs)
            Page page = diagram.Pages[0];

            // Helper to compute rectangle bounds of a shape
            static (double left, double right, double top, double bottom) GetBounds(Shape s)
            {
                double halfWidth = s.XForm.Width.Value / 2.0;
                double halfHeight = s.XForm.Height.Value / 2.0;
                double left = s.XForm.PinX.Value - halfWidth;
                double right = s.XForm.PinX.Value + halfWidth;
                double bottom = s.XForm.PinY.Value - halfHeight;
                double top = s.XForm.PinY.Value + halfHeight;
                return (left, right, top, bottom);
            }

            // Helper to test rectangle overlap
            static bool Overlaps(Shape a, Shape b)
            {
                var (aL, aR, aT, aB) = GetBounds(a);
                var (bL, bR, bT, bB) = GetBounds(b);
                bool horizontal = aL < bR && aR > bL;
                bool vertical = aB < bT && aT > bB;
                return horizontal && vertical;
            }

            // List to keep shapes that have been positioned without overlap
            var placedShapes = new System.Collections.Generic.List<Shape>();

            // Offset (in inches) to move a shape when overlap is detected
            const double offset = 0.5;

            // Iterate over all shapes on the page
            foreach (Shape shape in page.Shapes)
            {
                // Skip deleted shapes
                if (shape.Del == BOOL.True) continue;

                // Skip connectors (1‑D shapes) – they usually don't have text boxes
                if (shape.OneD) continue;

                // Attempt to place the shape without overlapping previously placed shapes
                bool moved;
                do
                {
                    moved = false;
                    foreach (Shape placed in placedShapes)
                    {
                        if (Overlaps(shape, placed))
                        {
                            // Shift the shape to the right by the offset
                            shape.XForm.PinX.Value += offset;
                            moved = true;
                            // Break to re‑check against all placed shapes after the move
                            break;
                        }
                    }
                } while (moved);

                // Add the now‑positioned shape to the collection
                placedShapes.Add(shape);
            }

            // Save the adjusted diagram to the output file in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any unexpected errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}