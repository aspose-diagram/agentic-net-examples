using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        // Simple overlap detection based on shape bounding boxes.
        private static bool IsOverlapping(Shape s1, Shape s2)
        {
            double left1 = s1.XForm.PinX.Value - s1.XForm.Width.Value / 2.0;
            double right1 = s1.XForm.PinX.Value + s1.XForm.Width.Value / 2.0;
            double bottom1 = s1.XForm.PinY.Value - s1.XForm.Height.Value / 2.0;
            double top1 = s1.XForm.PinY.Value + s1.XForm.Height.Value / 2.0;

            double left2 = s2.XForm.PinX.Value - s2.XForm.Width.Value / 2.0;
            double right2 = s2.XForm.PinX.Value + s2.XForm.Width.Value / 2.0;
            double bottom2 = s2.XForm.PinY.Value - s2.XForm.Height.Value / 2.0;
            double top2 = s2.XForm.PinY.Value + s2.XForm.Height.Value / 2.0;

            return left1 < right2 && right1 > left2 && bottom1 < top2 && top1 > bottom2;
        }

        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths (can be supplied via command‑line arguments).
                string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
                string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

                // Load the Visio diagram.
                Diagram diagram = new Diagram(inputPath);

                // Offset used to separate overlapping shapes (in inches).
                const double offsetX = 0.5;
                const double offsetY = 0.5;

                // Process each page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Keep a list of shapes that have already been positioned.
                    List<Shape> placedShapes = new List<Shape>();

                    // Iterate over all shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Skip connectors (1‑D shapes).
                        if (shape.OneD)
                            continue;

                        // Compare the current shape with all previously placed shapes.
                        bool hasOverlap;
                        do
                        {
                            hasOverlap = false;
                            foreach (Shape other in placedShapes)
                            {
                                if (IsOverlapping(shape, other))
                                {
                                    // Move the shape slightly to the right and down to resolve the collision.
                                    shape.Move(offsetX, offsetY);
                                    hasOverlap = true;
                                    // After moving, break to re‑evaluate against all placed shapes.
                                    break;
                                }
                            }
                        } while (hasOverlap);

                        // Add the shape to the list of placed shapes after it is positioned without overlap.
                        placedShapes.Add(shape);
                    }
                }

                // Save the modified diagram.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }