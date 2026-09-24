using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        // Simple container for shape geometry
        private class ShapeGeometry
        {
            public double PinX { get; set; }
            public double PinY { get; set; }
            public double Width { get; set; }
            public double Height { get; set; }
        }

        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";
                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Get the first page (assumes at least one page exists)
                Page page = diagram.Pages[0];

                // Store geometry of each shape before applying the theme
                var beforeGeometries = new System.Collections.Generic.Dictionary<long, ShapeGeometry>();
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    var geom = new ShapeGeometry
                    {
                        PinX = shape.XForm.PinX.Value,
                        PinY = shape.XForm.PinY.Value,
                        Width = shape.XForm.Width.Value,
                        Height = shape.XForm.Height.Value
                    };
                    beforeGeometries[shape.ID] = geom;
                }

                // Apply a preset theme to the page
                page.PresetTheme = PresetThemeValue.Bubble;
                // (Optional) set a variant if desired
                // page.PresetThemeVariant = PresetThemeVariantValue.Variant1;

                // Save the diagram after theme application (optional, but demonstrates persistence)
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                // Compare geometry after theme application
                const double tolerance = 1e-6;
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    if (!beforeGeometries.TryGetValue(shape.ID, out ShapeGeometry before))
                    {
                        // New shape introduced by the theme (unlikely); ignore or handle as needed
                        continue;
                    }

                    bool mismatch =
                        Math.Abs(shape.XForm.PinX.Value - before.PinX) > tolerance ||
                        Math.Abs(shape.XForm.PinY.Value - before.PinY) > tolerance ||
                        Math.Abs(shape.XForm.Width.Value - before.Width) > tolerance ||
                        Math.Abs(shape.XForm.Height.Value - before.Height) > tolerance;

                    if (mismatch)
                    {
                        throw new Exception($"Geometry changed for shape ID {shape.ID} after applying theme.");
                    }
                }

                Console.WriteLine("All shape geometries remain unchanged after applying the preset theme.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }