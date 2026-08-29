using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Paths to the two Visio files to compare.
                // Replace with actual file locations or pass via command‑line arguments.
                string oldFilePath = args.Length > 0 ? args[0] : "oldDiagram.vsdx";
                string newFilePath = args.Length > 1 ? args[1] : "newDiagram.vsdx";

                // Load the diagrams.
                Diagram oldDiagram = new Diagram(oldFilePath);
                Diagram newDiagram = new Diagram(newFilePath);

                // Build dictionaries of shapes from the old diagram.
                var oldShapes = BuildShapeDictionary(oldDiagram);
                var newShapes = BuildShapeDictionary(newDiagram);

                // Track IDs that have been processed.
                var processedIds = new System.Collections.Generic.HashSet<long>();

                // Detect added and modified shapes.
                foreach (var kvp in newShapes)
                {
                    long shapeId = kvp.Key;
                    Shape newShape = kvp.Value;

                    if (!oldShapes.TryGetValue(shapeId, out Shape oldShape))
                    {
                        Console.WriteLine($"Added Shape: ID={shapeId}, NameU=\"{newShape.NameU}\"");
                    }
                    else
                    {
                        // Compare selected properties to decide if modified.
                        if (IsShapeModified(oldShape, newShape))
                        {
                            Console.WriteLine($"Modified Shape: ID={shapeId}, NameU=\"{newShape.NameU}\"");
                        }
                        processedIds.Add(shapeId);
                    }
                }

                // Detect removed shapes.
                foreach (var kvp in oldShapes)
                {
                    long shapeId = kvp.Key;
                    if (!processedIds.Contains(shapeId) && !newShapes.ContainsKey(shapeId))
                    {
                        Shape oldShape = kvp.Value;
                        Console.WriteLine($"Removed Shape: ID={shapeId}, NameU=\"{oldShape.NameU}\"");
                    }
                }

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }

        // Builds a dictionary of all shapes in a diagram keyed by their unique ID.
        private static System.Collections.Generic.Dictionary<long, Shape> BuildShapeDictionary(Diagram diagram)
        {
            var dict = new System.Collections.Generic.Dictionary<long, Shape>();
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    dict[shape.ID] = shape;
                }
            }
            return dict;
        }

        // Determines whether two shapes differ in any of the examined properties.
        private static bool IsShapeModified(Shape oldShape, Shape newShape)
        {
            // Compare NameU.
            if (!string.Equals(oldShape.NameU, newShape.NameU, StringComparison.Ordinal))
                return true;

            // Compare plain text.
            string oldText = oldShape.Text?.Value?.Text ?? string.Empty;
            string newText = newShape.Text?.Value?.Text ?? string.Empty;
            if (!string.Equals(oldText, newText, StringComparison.Ordinal))
                return true;

            // Compare position (PinX, PinY) with a small tolerance.
            const double tolerance = 0.0001;
            if (Math.Abs(oldShape.XForm.PinX.Value - newShape.XForm.PinX.Value) > tolerance ||
                Math.Abs(oldShape.XForm.PinY.Value - newShape.XForm.PinY.Value) > tolerance)
                return true;

            // Compare size (Width, Height) with tolerance.
            if (Math.Abs(oldShape.XForm.Width.Value - newShape.XForm.Width.Value) > tolerance ||
                Math.Abs(oldShape.XForm.Height.Value - newShape.XForm.Height.Value) > tolerance)
                return true;

            // Compare rotation angle (stored in radians).
            if (Math.Abs(oldShape.XForm.Angle.Value - newShape.XForm.Angle.Value) > tolerance)
                return true;

            // No differences detected.
            return false;
        }
    }