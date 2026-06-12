using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths (adjust as needed)
                string inputPath = "input.vsdx";
                string outputPath = "output_resolved.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Work with the first page (modify as needed for multiple pages)
                Page page = diagram.Pages[0];

                // Collect all non-deleted shapes that contain text
                List<Shape> textShapes = new List<Shape>();
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Del == BOOL.False && !string.IsNullOrWhiteSpace(shape.Text.Value.ToString()))
                    {
                        textShapes.Add(shape);
                    }
                }

                // Simple pairwise overlap detection and resolution
                for (int i = 0; i < textShapes.Count; i++)
                {
                    Shape shapeA = textShapes[i];
                    for (int j = i + 1; j < textShapes.Count; j++)
                    {
                        Shape shapeB = textShapes[j];

                        if (IsOverlapping(shapeA, shapeB))
                        {
                            // Resolve overlap by shifting shapeB to the right by its own width
                            double currentPinX = shapeB.XForm.PinX.Value;
                            double width = shapeB.XForm.Width.Value;
                            shapeB.XForm.PinX.Value = currentPinX + width;
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        // Determines whether two shapes' bounding boxes intersect
        private static bool IsOverlapping(Shape a, Shape b)
        {
            // Compute half dimensions for easier calculations
            double aHalfWidth = a.XForm.Width.Value / 2.0;
            double aHalfHeight = a.XForm.Height.Value / 2.0;
            double bHalfWidth = b.XForm.Width.Value / 2.0;
            double bHalfHeight = b.XForm.Height.Value / 2.0;

            // Bounding box edges for shape A
            double aLeft = a.XForm.PinX.Value - aHalfWidth;
            double aRight = a.XForm.PinX.Value + aHalfWidth;
            double aBottom = a.XForm.PinY.Value - aHalfHeight;
            double aTop = a.XForm.PinY.Value + aHalfHeight;

            // Bounding box edges for shape B
            double bLeft = b.XForm.PinX.Value - bHalfWidth;
            double bRight = b.XForm.PinX.Value + bHalfWidth;
            double bBottom = b.XForm.PinY.Value - bHalfHeight;
            double bTop = b.XForm.PinY.Value + bHalfHeight;

            // Check for separation on any axis
            bool separated = aRight <= bLeft || aLeft >= bRight || aTop <= bBottom || aBottom >= bTop;
            return !separated;
        }
    }