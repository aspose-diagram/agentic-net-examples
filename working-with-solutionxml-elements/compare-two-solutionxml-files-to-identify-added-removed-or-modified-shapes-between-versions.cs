using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Paths to the two Visio files containing SolutionXML data
                string filePathOld = "oldDiagram.vsdx";
                string filePathNew = "newDiagram.vsdx";

                // Load the diagrams
                Diagram oldDiagram = new Diagram(filePathOld);
                Diagram newDiagram = new Diagram(filePathNew);

                // Build dictionaries of shapes keyed by PageIndex + Shape NameU
                var oldShapes = BuildShapeDictionary(oldDiagram);
                var newShapes = BuildShapeDictionary(newDiagram);

                // Identify added shapes
                foreach (var key in newShapes.Keys)
                {
                    if (!oldShapes.ContainsKey(key))
                    {
                        Console.WriteLine($"Added Shape: Page {newShapes[key].PageIndex}, NameU = {newShapes[key].Shape.NameU}");
                    }
                }

                // Identify removed shapes
                foreach (var key in oldShapes.Keys)
                {
                    if (!newShapes.ContainsKey(key))
                    {
                        Console.WriteLine($"Removed Shape: Page {oldShapes[key].PageIndex}, NameU = {oldShapes[key].Shape.NameU}");
                    }
                }

                // Identify modified shapes (present in both but with differing properties)
                foreach (var key in oldShapes.Keys)
                {
                    if (newShapes.TryGetValue(key, out var newInfo))
                    {
                        var oldInfo = oldShapes[key];
                        if (!ShapeSignaturesEqual(oldInfo.Shape, newInfo.Shape))
                        {
                            Console.WriteLine($"Modified Shape: Page {oldInfo.PageIndex}, NameU = {oldInfo.Shape.NameU}");
                            Console.WriteLine($"  Old: {GetShapeSignature(oldInfo.Shape)}");
                            Console.WriteLine($"  New: {GetShapeSignature(newInfo.Shape)}");
                        }
                    }
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        // Helper to collect shapes from all pages into a dictionary
        private static Dictionary<string, ShapeInfo> BuildShapeDictionary(Diagram diagram)
        {
            var dict = new Dictionary<string, ShapeInfo>(StringComparer.OrdinalIgnoreCase);
            for (int p = 0; p < diagram.Pages.Count; p++)
            {
                Page page = diagram.Pages[p];
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True) continue;

                    // Use page index and universal name as a composite key
                    string key = $"{p}|{shape.NameU}";
                    dict[key] = new ShapeInfo { PageIndex = p, Shape = shape };
                }
            }
            return dict;
        }

        // Simple comparison of selected shape properties
        private static bool ShapeSignaturesEqual(Shape a, Shape b)
        {
            if (a == null || b == null) return false;

            // Compare master name
            string masterA = a.Master?.Name ?? string.Empty;
            string masterB = b.Master?.Name ?? string.Empty;
            if (!masterA.Equals(masterB, StringComparison.OrdinalIgnoreCase)) return false;

            // Compare text content
            string textA = a.Text?.Value?.Text ?? string.Empty;
            string textB = b.Text?.Value?.Text ?? string.Empty;
            if (!textA.Equals(textB, StringComparison.Ordinal)) return false;

            // Compare position and size
            if (a.XForm.PinX.Value != b.XForm.PinX.Value) return false;
            if (a.XForm.PinY.Value != b.XForm.PinY.Value) return false;
            if (a.XForm.Width.Value != b.XForm.Width.Value) return false;
            if (a.XForm.Height.Value != b.XForm.Height.Value) return false;

            // Compare fill and line colors (foreground)
            string fillA = a.Fill?.FillForegnd?.Value ?? string.Empty;
            string fillB = b.Fill?.FillForegnd?.Value ?? string.Empty;
            if (!fillA.Equals(fillB, StringComparison.OrdinalIgnoreCase)) return false;

            string lineA = a.Line?.LineColor?.Value ?? string.Empty;
            string lineB = b.Line?.LineColor?.Value ?? string.Empty;
            if (!lineA.Equals(lineB, StringComparison.OrdinalIgnoreCase)) return false;

            return true;
        }

        // Produce a readable signature for a shape
        private static string GetShapeSignature(Shape shape)
        {
            string master = shape.Master?.Name ?? "None";
            string text = shape.Text?.Value?.Text ?? "";
            return $"Master={master}, Text=\"{text}\", PinX={shape.XForm.PinX.Value}, PinY={shape.XForm.PinY.Value}, Width={shape.XForm.Width.Value}, Height={shape.XForm.Height.Value}, Fill={shape.Fill?.FillForegnd?.Value}, Line={shape.Line?.LineColor?.Value}";
        }

        // Simple holder for shape with its page index
        private class ShapeInfo
        {
            public int PageIndex { get; set; }
            public Shape Shape { get; set; }
        }
    }