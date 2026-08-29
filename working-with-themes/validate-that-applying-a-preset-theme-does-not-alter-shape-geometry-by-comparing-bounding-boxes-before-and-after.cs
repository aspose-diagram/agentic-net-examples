using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

// Simple struct to hold bounding box coordinates
    struct BoundingBox
    {
        public double Left;
        public double Right;
        public double Top;
        public double Bottom;
    }

    class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file (replace with actual file path)
                const string inputPath = "input.vsdx";
                const string outputPath = "output_with_theme.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Work with the first page (adjust index if needed)
                Page page = diagram.Pages[0];

                // Store original bounding boxes keyed by shape ID
                var originalBoxes = new System.Collections.Generic.Dictionary<long, BoundingBox>();

                // Capture geometry before applying the theme
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    BoundingBox box = GetBoundingBox(shape);
                    originalBoxes[shape.ID] = box;
                }

                // Apply a preset theme to the page
                page.PresetTheme = PresetThemeValue.Bubble;
                page.PresetThemeVariant = PresetThemeVariantValue.Variant1;

                // Optionally save the diagram to verify the theme is applied
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                // Compare geometry after theme application
                bool allMatch = true;
                const double epsilon = 1e-4; // tolerance for floating‑point comparison

                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Del == BOOL.True)
                        continue;

                    if (!originalBoxes.TryGetValue(shape.ID, out BoundingBox before))
                    {
                        Console.WriteLine($"Shape ID {shape.ID} was not present before theme application.");
                        allMatch = false;
                        continue;
                    }

                    BoundingBox after = GetBoundingBox(shape);

                    if (Math.Abs(before.Left - after.Left) > epsilon ||
                        Math.Abs(before.Right - after.Right) > epsilon ||
                        Math.Abs(before.Top - after.Top) > epsilon ||
                        Math.Abs(before.Bottom - after.Bottom) > epsilon)
                    {
                        Console.WriteLine($"Geometry changed for Shape ID {shape.ID}.");
                        Console.WriteLine($"Before: L={before.Left}, R={before.Right}, T={before.Top}, B={before.Bottom}");
                        Console.WriteLine($"After : L={after.Left}, R={after.Right}, T={after.Top}, B={after.Bottom}");
                        allMatch = false;
                    }
                }

                if (allMatch)
                {
                    Console.WriteLine("All shape geometries remain unchanged after applying the preset theme.");
                }
                else
                {
                    throw new Exception("One or more shapes changed geometry after applying the preset theme.");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        // Helper method to compute the bounding box of a shape
        private static BoundingBox GetBoundingBox(Shape shape)
        {
            double pinX = shape.XForm.PinX.Value;
            double pinY = shape.XForm.PinY.Value;
            double width = shape.XForm.Width.Value;
            double height = shape.XForm.Height.Value;

            return new BoundingBox
            {
                Left = pinX - width / 2.0,
                Right = pinX + width / 2.0,
                Top = pinY + height / 2.0,
                Bottom = pinY - height / 2.0
            };
        }
    }